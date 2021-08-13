using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface.Repository
{
    public interface IUpAccountRepository
    {
        Task<TransactionData> GetTransactionsFromUpApi(string startDate, string endDate);
    }
}