namespace EventSourcing.Web.TransactionsContracts.Accounts.ValueObjects
{
    public class AllAccountsSummary
    {
        public int TotalCount { get; set; }

        public decimal TotalBalance { get; set; }

        public int TotalTransactionsCount { get; set; }
    }
}
