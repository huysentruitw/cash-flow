using System;

namespace CashFlow.Command.Abstractions.Exceptions
{
    public sealed class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public sealed class FailedToDeleteLastTransactionException : Exception
    {
        public FailedToDeleteLastTransactionException()
            : base("Failed to delete last transaction")
        {
        }
    }
}
