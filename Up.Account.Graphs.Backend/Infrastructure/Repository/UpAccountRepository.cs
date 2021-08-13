using Domain.Config.ApiSettings;
using Domain.Model;
using Infrastructure.Interface.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Infrastructure.Repository
{
    public class UpAccountRepository : IUpAccountRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly UpApiSettings _upApiSettings;

        public UpAccountRepository(IOptions<UpApiSettings> upApiSettings, IHttpClientFactory clientFactory)
        {
            _upApiSettings = upApiSettings.Value ?? throw new ArgumentNullException(nameof(upApiSettings));
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task<TransactionData> GetTransactionsFromUpApi(string startDate, string endDate)
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
                var list = JsonConvert.DeserializeObject<TransactionData>(content);
                return list;
            }

            return null;
        }
    }
}