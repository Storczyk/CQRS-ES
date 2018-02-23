using System;
using EventSourcing.Web.ClientsContracts.Events;

namespace EventSourcing.Web.Transactions.Views.Clients
{
    /*public class ClientsViewProjection : ViewProjection<ClientsView, Guid>
    {
        public ClientsViewProjection()
        {
            ProjectEvent<ClientCreatedEvent>(ev => ev.Id, (view, @event) => view.ApplyEvent(@event));
            ProjectEvent<ClientDeletedEvent>(ev => ev.Id, (view, @event) => view.ApplyEvent(@event));
            ProjectEvent<ClientUpdatedEvent>(ev => ev.Id, (view, @event) => view.ApplyEvent(@event));
        }
    }*/
}
