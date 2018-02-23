﻿using System;
using System.Threading.Tasks;
using EventSourcing.Web.Domain.Commands;
using EventSourcing.Web.Domain.Queries;
using EventSourcing.Web.TransactionsContracts.Accounts.Commands;
using EventSourcing.Web.TransactionsContracts.Accounts.Queries;
using EventSourcing.Web.TransactionsContracts.Accounts.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Web.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public AccountsController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        // GET api/values
        [HttpGet("{accountId}")]
        public Task<AccountSummary> Get(Guid accountId)
        {
            return _queryBus.Send<GetAccount, AccountSummary>(new GetAccount(accountId));
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]CreateNewAccount command)
        {
            await _commandBus.Send(command);
        }
    }
}