using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Account.Graphs.Backend.Queries;

namespace Up.Account.Graphs.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpAccountController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Route("GetTransactions/{startDate}/{endDate}")]
        [HttpGet]
        public async Task<IActionResult> GetTransactionsWithinDates(DateTime startDate, DateTime endDate)
        {
            var result = await _mediator.Send(new GetTransactionsQuery
            {
                startDate = startDate,
                endDate = endDate
            });

            if (result.IsSuccess)
                return Ok(result.Data);

            return StatusCode(result.ServiceProcessResultCode, result);
        }
    }
}