using System;

namespace CashFlow.Query.Abstractions.Models
{
    public sealed class FinancialYear
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset DateCreated { get; set; }
    }
}
