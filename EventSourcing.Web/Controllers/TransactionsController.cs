using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Domain.Commands;
using EventSourcing.Web.Domain.Queries;
using EventSourcing.Web.TransactionsContracts.Transactions.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace EventSourcing.Web.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly IHostedService hostedService;

        public TransactionsController(ICommandBus commandBus, IQueryBus queryBus, IHostedService hostedService)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
            this.hostedService = hostedService;
        }

        [HttpPost]
        public async Task Post([FromBody]MakeTransfer command)
        {
            await _commandBus.Send(command);
        }

        public IActionResult Action()
        {
            int a = 1 + 1;

            var d = hostedService.StartAsync(new CancellationToken());
            //var res = await hostedService.ExecuteAsync(new CancellationToken());


            int b = 2 + a;
            return Ok(a);
        }
    }
}
