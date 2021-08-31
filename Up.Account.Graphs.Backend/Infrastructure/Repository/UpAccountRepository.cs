using Domain.Config.ApiSettings;
using Domain.Model;
using Infrastructure.Interface.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Serilog;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class UpAccountRepository : IUpAccountRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly UpApiSettings _upApiSettings;
        private readonly ILogger<UpAccountRepository> _log;

        public UpAccountRepository(IOptions<UpApiSettings> upApiSettings, IHttpClientFactory clientFactory, ILogger<UpAccountRepository> log)
        {
            _upApiSettings = upApiSettings.Value ?? throw new ArgumentNullException(nameof(upApiSettings));
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public async Task<TransactionData> GetTransactionsFromUpApi(string startDate, string endDate)
        {
            try
            {
                var client = _clientFactory.CreateClient(_upApiSettings.ClientName);

                var uri = _upApiSettings.GetTransactionsListUri;

                var startDateTime = DateTime.Parse(startDate).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
                var endDateTime = DateTime.Parse(endDate).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");

                uri = string.Format(uri, startDateTime, endDateTime);

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _upApiSettings.BaseClient + uri);

                client.DefaultRequestHeaders.Add("Authorization", $"{_upApiSettings.ApiKey["Header"]} {_upApiSettings.ApiKey["Key"]}");

                var result = await client.SendAsync(httpRequestMessage);

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var transactionData = JsonConvert.DeserializeObject<TransactionData>(content);

                    foreach (var transaction in transactionData.Data)
                    {
                        var categoryName = "";
                        // track the description/id with the value
                        if (transaction.Relationships?.Category?.Data?.Id != null)
                        {
                            if (!transactionData.Categories.ContainsKey(transaction.Relationships.Category.Data.Id))
                            {
                                transactionData.Categories.Add(transaction.Relationships.Category.Data.Id, transaction.Attributes.Amount.Value);
                                categoryName = transaction.Relationships.Category.Data.Id;
                            }
                            else
                            {
                                transactionData.Categories[transaction.Relationships.Category.Data.Id] += transaction.Attributes.Amount.Value;
                                categoryName = transaction.Relationships.Category.Data.Id;
                            }
                        }
                        else
                        {
                            if (!transactionData.Categories.ContainsKey(transaction.Attributes.Description))
                            {
                                transactionData.Categories.Add(transaction.Attributes.Description, transaction.Attributes.Amount.Value);
                                categoryName = transaction.Attributes.Description;
                            }
                            else
                            {
                                transactionData.Categories[transaction.Attributes.Description] += transaction.Attributes.Amount.Value;
                                categoryName = transaction.Attributes.Description;
                            }
                        }

                        // track the inflow/outflow of money
                        if (transaction.Attributes.Amount.Value > 0)
                        {
                            AddToTransactionList(transactionData, transaction, categoryName, true);
                        }
                        else
                        {
                            AddToTransactionList(transactionData, transaction, categoryName, false);
                        }
                    }

                    return transactionData;
                }

                return null;
            }
            catch (Exception e)
            {
                _log.LogError($"Exception thrown during {nameof(GetTransactionsFromUpApi)} - {e.Message}");
                return null;
            }
        }

        private void AddToTransactionList(TransactionData transactionData, TransactionEntry transaction, string categoryName, bool isPositiveFlag)
        {
            if (isPositiveFlag)
            {
                if (!transactionData.IncomingTransactions.ContainsKey(categoryName))
                {
                    transactionData.IncomingTransactions.Add(categoryName, transaction.Attributes.Amount.Value);
                    transactionData.TotalInflow += transaction.Attributes.Amount.Value;
                }
                else
                {
                    transactionData.IncomingTransactions[categoryName] += transaction.Attributes.Amount.Value;
                    transactionData.TotalInflow += transaction.Attributes.Amount.Value;
                }
            }
            else
            {
                if (!transactionData.OutgoingTransactions.ContainsKey(categoryName))
                {
                    transactionData.OutgoingTransactions.Add(categoryName, transaction.Attributes.Amount.Value);
                    transactionData.TotalOutflow -= transaction.Attributes.Amount.Value;
                }
                else
                {
                    transactionData.OutgoingTransactions[categoryName] += transaction.Attributes.Amount.Value;
                    transactionData.TotalOutflow -= transaction.Attributes.Amount.Value;
                }
            }
        }

        // relationship -> category -> data -> id
    }
}