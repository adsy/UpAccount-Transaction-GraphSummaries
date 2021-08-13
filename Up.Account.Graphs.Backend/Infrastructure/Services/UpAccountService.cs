using Domain.Model;
using Infrastructure.Interface;
using Infrastructure.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UpAccountService : IUpAccountService
    {
        private readonly IUpAccountRepository _upAccountRepository;

        public UpAccountService(IUpAccountRepository upAccountRepository)
        {
            _upAccountRepository = upAccountRepository ?? throw new ArgumentNullException(nameof(upAccountRepository));
        }

        public async Task<ServiceProcessResult<TransactionData>> GetAccountTransactions(string startDate, string endDate)
        {
            var fnResult = new ServiceProcessResult<TransactionData>
            {
                ServiceProcessResultCode = (int)HttpStatusCode.OK
            };

            try
            {
                var result = await _upAccountRepository.GetTransactionsFromUpApi(startDate, endDate);

                if (result == null || !result.Data.Any())
                {
                    fnResult.ServiceProcessResultCode = (int)HttpStatusCode.NotFound;
                    fnResult.ServiceProcessResultMessage = "No transactions were found.";
                }
                else
                {
                    fnResult.Data = result;
                }

                return fnResult;
            }
            catch (Exception e)
            {
                fnResult.ServiceProcessResultCode = (int)HttpStatusCode.InternalServerError;
                fnResult.ServiceProcessResultMessage = e.Message;
                return fnResult;
            }
        }
    }
}