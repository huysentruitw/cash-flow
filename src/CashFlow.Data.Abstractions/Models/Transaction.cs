using System;

namespace CashFlow.Data.Abstractions.Models
{
    public sealed class Transaction
    {
        public Guid Id { get; set; }

        public DateTimeOffset TransactionDate { get; set; }

        public int? EvidenceNumber { get; set; }

        public Guid FinancialYearId { get; set; }

        public Guid AccountId { get; set; }

        public Guid? SupplierId { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public DateTimeOffset? DateModified { get; set; }

        public long AmountInCents { get; set; }

        public bool IsInternalTransfer { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }
    }
}
