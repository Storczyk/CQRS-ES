using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventSourcing.Web.Clients.Domain.Clients;
using EventSourcing.Web.ClientsContracts.Commands;
using EventSourcing.Web.ClientsContracts.Queries;
using EventSourcing.Web.ClientsContracts.ValueObjects;
using EventSourcing.Web.Domain.Commands;
using EventSourcing.Web.Domain.Queries;
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
        public Task<List<Client>> Get()
        {
            return _queryBus.Send<GetClients, List<Client>>(new GetClients());
        }

        [HttpGet("{id}")]
        public Task<Client> Get(Guid id)
        {
            return _queryBus.Send<GetClient, Client>(new GetClient(id));
        }

        [HttpPost]
        public async Task Post([FromBody]CreateClient command)
        {
            await _commandBus.Send(command);
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody]ClientInfo clientInfo)
        {
            await _commandBus.Send(new UpdateClient(id, clientInfo));
        }
    }
}
