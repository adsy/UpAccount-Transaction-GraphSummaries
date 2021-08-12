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
        Task<ServiceProcessResult<List<TransactionEntry>>> GetAccountTransactions(DateTime startDate, DateTime endDate);
    }
}