using System;

namespace CashFlow.Data.Abstractions.Models
{
    public sealed class FinancialYear
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset DateCreated { get; set; }
    }
}
