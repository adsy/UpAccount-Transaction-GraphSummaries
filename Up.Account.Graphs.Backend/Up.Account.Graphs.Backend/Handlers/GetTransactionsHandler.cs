using Domain.Model;
using Infrastructure.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Up.Account.Graphs.Backend.Queries;

namespace Up.Account.Graphs.Backend.Handlers
{
    public class GetTransactionsHandler : IRequestHandler<GetTransactionsQuery, ServiceProcessResult<List<TransactionEntry>>>
    {
        private readonly IUpAccountService _upAccountService;

        public GetTransactionsHandler(IUpAccountService upAccountService)
        {
            _upAccountService = upAccountService ?? throw new ArgumentNullException(nameof(upAccountService));
        }

        public async Task<ServiceProcessResult<List<TransactionEntry>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var result = await _upAccountService.GetAccountTransactions(request.startDate, request.endDate);

            return result;
        }
    }
}