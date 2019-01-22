using System;

namespace CashFlow.Data.Abstractions.Models
{
    public sealed class TransactionCode
    {
        public Guid TransactionId { get; set; }

        public string CodeName { get; set; }

        public DateTimeOffset DateAssigned { get; set; }
    }
}
