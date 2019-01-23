using System;

namespace CashFlow.GraphApi.Schema
{
    internal sealed class TransactionCode
    {
        public Guid TransactionId { get; set; }

        public string CodeName { get; set; }

        public DateTimeOffset DateAssigned { get; set; }
    }
}
