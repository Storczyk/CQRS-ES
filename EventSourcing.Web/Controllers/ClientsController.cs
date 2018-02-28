using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventSourcing.Web.ClientsContracts.Commands;
using EventSourcing.Web.ClientsContracts.Events;
using EventSourcing.Web.ClientsContracts.Queries;
using EventSourcing.Web.ClientsContracts.ValueObjects;
using EventSourcing.Web.Domain.Commands;
using EventSourcing.Web.Domain.Events;
using EventSourcing.Web.Domain.Queries;
using EventSourcing.Web.TransactionsContracts.Accounts.Queries;
using EventSourcing.Web.TransactionsContracts.Accounts.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Web.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly IMediator _eventBus;

        public ClientsController(ICommandBus commandBus, IQueryBus queryBus, IMediator eventBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
            _eventBus = eventBus;
        }


        [HttpGet]
        public Task<List<ClientListItem>> Get()
        {
            return _queryBus.Send<GetClients, List<ClientListItem>>(new GetClients());
        }


        [HttpGet("{id}")]
        public Task<ClientItem> Get(Guid id)
        {
            return _queryBus.Send<GetClient, ClientItem>(new GetClient(id));
        }

        [HttpGet]
        [Route("{id}/accounts")]
        public Task<IEnumerable<AccountSummary>> GetAccounts(Guid id)
        {
            return _queryBus.Send<GetAccounts, IEnumerable<AccountSummary>>(new GetAccounts(id));
        }

        [HttpPost]
        public async Task Post([FromBody]CreateClient command)
        {
            //await _eventBus.Publish(new ClientCreatedEvent(Guid.Empty, new ClientInfo("f", "f"), Guid.NewGuid()));
            await _commandBus.Send(command);
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody]ClientInfo clientInfo)
        {
            await _commandBus.Send(new UpdateClient(id, clientInfo));
        }
    }
}
