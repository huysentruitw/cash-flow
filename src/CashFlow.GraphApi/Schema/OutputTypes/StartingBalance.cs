using System;

namespace CashFlow.GraphApi.Schema
{
    public sealed class StartingBalance
    {
        public Guid AccountId { get; set; }

        public Guid FinancialYearId { get; set; }

        public long StartingBalanceInCents { get; set; }
    }
}
