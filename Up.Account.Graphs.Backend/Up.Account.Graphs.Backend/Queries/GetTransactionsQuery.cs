using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Up.Account.Graphs.Backend.Queries
{
    public class GetTransactionsQuery : IRequest<ServiceProcessResult<List<TransactionEntry>>>
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}