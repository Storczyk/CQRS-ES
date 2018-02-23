using System;
using EventSourcing.Web.Domain.Commands;

namespace EventSourcing.Web.ClientsContracts.Commands
{
    public class DeleteClient : ICommand
    {
        public Guid Id { get; }

        public DeleteClient(Guid id)
        {
            Id = id;
        }
    }
}
