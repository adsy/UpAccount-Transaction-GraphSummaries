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

        public async Task<ServiceProcessResult<List<TransactionEntry>>> GetAccountTransactions(DateTime startDate, DateTime endDate)
        {
            var fnResult = new ServiceProcessResult<List<TransactionEntry>>
            {
                ServiceProcessResultCode = (int)HttpStatusCode.OK
            };

            try
            {
                var result = await _upAccountRepository.GetTransactionsFromUpApi(startDate, endDate);

                if (result == null || !result.Any())
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