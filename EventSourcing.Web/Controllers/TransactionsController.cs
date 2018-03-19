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
        //private readonly IHostedService hostedService;

        private Task<bool> _executingTask;
        private CancellationTokenSource _cts;

        public TransactionsController(ICommandBus commandBus, IQueryBus queryBus, IHostedService hostedService)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
            //this.hostedService = hostedService;
        }

        [HttpPost]
        public async Task Post([FromBody]MakeTransfer command)
        {
            await _commandBus.Send(command);
        }

        public IActionResult Action()
        {
            int a = 1 + 1;
            ExecuteAsync(new CancellationToken());
            //var res = await Method(new CancellationToken());
            
            int b = 2 + a;
            return Ok(a);
        }

        private Task<bool> ExecuteAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _executingTask = Method(_cts.Token);

            if (_executingTask.IsCompleted)
            {
                return Task.FromResult(_executingTask.Result);
            }
            else
            {
                return new Task<bool>(() => false, default(CancellationToken), TaskCreationOptions.None);
            }
        }

        private async Task<bool> Method(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                //operations
                return true;
            }
            return false;
        }
    }
}
