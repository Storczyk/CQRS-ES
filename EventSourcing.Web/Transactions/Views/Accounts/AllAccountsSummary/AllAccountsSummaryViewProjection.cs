using System;
using EventSourcing.Web.TransactionsContracts.Accounts.Events;
using EventSourcing.Web.TransactionsContracts.Transactions.Events;

namespace EventSourcing.Web.Transactions.Views.Accounts.AllAccountsSummary
{
    /*public class AllAccountsSummaryViewProjection : ViewProjection<AllAccountsSummaryView, Guid>
    {
        public AllAccountsSummaryViewProjection()
        {
            ProjectEventToSingleRecord<NewAccountCreatedEvent>((view, @event) => view.ApplyEvent(@event));
            ProjectEventToSingleRecord<NewInTransactionRecorded>((view, @event) => view.ApplyEvent(@event));
            ProjectEventToSingleRecord<NewOutTransactionRecorded>((view, @event) => view.ApplyEvent(@event));
        }

        ViewProjection<AllAccountsSummaryView, Guid> ProjectEventToSingleRecord<TEvent>(Action<AllAccountsSummaryView, TEvent> handler) where TEvent : class
        {
            return ProjectEvent((documentSession, ev) => FindIdOfRecord(documentSession) ?? Guid.NewGuid(), handler);
        }

        Guid? FindIdOfRecord(IDocumentSession documentSession)
        {
            return documentSession.Query<AllAccountsSummaryView>()
                .Select(t => (Guid?)t.Id).FirstOrDefault();
        }
    }*/
}
