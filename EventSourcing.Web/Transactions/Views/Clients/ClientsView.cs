using System;
using EventSourcing.Web.ClientsContracts.Events;

namespace EventSourcing.Web.Transactions.Views.Clients
{
   /* public class ClientsView : ViewProjection<ClientsView, Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        internal void ApplyEvent(ClientCreatedEvent @event)
        {
            Id = @event.Id;
            Name = @event.Data.Name;
            IsDeleted = false;
        }

        internal void ApplyEvent(ClientUpdatedEvent @event)
        {
            Name = @event.Data.Name;
        }

        internal void ApplyEvent(ClientDeletedEvent @event)
        {
            IsDeleted = true;
        }
    }*/
}
