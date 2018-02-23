using System;
using EventSourcing.Web.ClientsContracts.ValueObjects;
using EventSourcing.Web.Domain.Commands;

namespace EventSourcing.Web.ClientsContracts.Commands
{
    public class UpdateClient : ICommand
    {
        public Guid Id { get; set; }
        public ClientInfo Data { get; }

        public UpdateClient(Guid id, ClientInfo data)
        {
            Id = id;
            Data = data;
        }
    }
}
