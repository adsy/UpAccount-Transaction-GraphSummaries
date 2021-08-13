using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IUpAccountService
    {
        Task<ServiceProcessResult<TransactionData>> GetAccountTransactions(string startDate, string endDate);
    }
}