using System;

namespace CashFlow.Command.Abstractions.Exceptions
{
    public sealed class FinancialYearNotFoundException : Exception
    {
        public FinancialYearNotFoundException(Guid id)
            : base($"Financial year with id {id} not found")
        {
            Id = id;
        }

        public Guid Id { get; }
    }

}
