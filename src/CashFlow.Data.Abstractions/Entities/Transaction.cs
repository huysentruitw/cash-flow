using System;
using System.Collections.Generic;

namespace CashFlow.Data.Abstractions.Entities
{
    public sealed class Transaction
    {
        public Guid Id { get; set; }

        public DateTimeOffset TransactionDate { get; set; }

        public string EvidenceNumber { get; set; }

        public Guid FinancialYearId { get; set; }

        public Guid AccountId { get; set; }

        public Guid? SupplierId { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public DateTimeOffset? DateModified { get; set; }

        public long AmountInCents { get; set; }

        public bool IsInternalTransfer { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public ICollection<TransactionCode> Codes { get; set; }
    }
}
