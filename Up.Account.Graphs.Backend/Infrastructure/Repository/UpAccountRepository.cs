using Domain.Config.ApiSettings;
using Domain.Model;
using Infrastructure.Interface.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UpAccountRepository : IUpAccountRepository
    {
        private readonly UpApiSettings _upApiSettings;

        public UpAccountRepository(IOptions<UpApiSettings> upApiSettings)
        {
            _upApiSettings = upApiSettings.Value ?? throw new ArgumentNullException(nameof(upApiSettings));
        }

        public Task<List<TransactionEntry>> GetTransactionsFromUpApi(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}