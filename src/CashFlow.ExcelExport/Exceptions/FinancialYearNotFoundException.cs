using System;

namespace CashFlow.ExcelExport.Exceptions
{
    internal sealed class FinancialYearNotFoundException : Exception
    {
        public FinancialYearNotFoundException(Guid financialYearId)
            : base($"Financial year with id {financialYearId} not found")
        {
            FinancialYearId = financialYearId;
        }

        public Guid FinancialYearId { get; }
    }
}
