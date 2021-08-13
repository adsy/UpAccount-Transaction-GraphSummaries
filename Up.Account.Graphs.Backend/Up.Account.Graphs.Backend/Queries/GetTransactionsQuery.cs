using Domain.Model;
using MediatR;
using System.Collections.Generic;

namespace Up.Account.Graphs.Backend.Queries
{
    public class GetTransactionsQuery : IRequest<ServiceProcessResult<TransactionData>>
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}