using System.Threading.Tasks;
using EventSourcing.Web.Domain.Commands;
using EventSourcing.Web.Domain.Queries;
using EventSourcing.Web.TransactionsContracts.Transactions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Web.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public TransactionsController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]MakeTransfer command)
        {
            await _commandBus.Send(command);
        }
    }
}
