using System;

namespace CashFlow.Query.Abstractions.Exceptions
{
    public sealed class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException(Guid id)
            : base($"Transaction with id {id} not found")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
