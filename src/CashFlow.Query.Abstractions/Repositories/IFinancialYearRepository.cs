using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions.Entities;

namespace CashFlow.Query.Abstractions.Repositories
{
    public interface IFinancialYearRepository
    {
        Task<FinancialYear> GetFinancialYear(Guid financialYearId);
        Task<FinancialYear[]> GetFinancialYears();
        Task<IReadOnlyDictionary<Guid, FinancialYear>> GetFinancialYearsInBatch(IReadOnlyList<Guid> financialYearIds, CancellationToken cancellationToken);
        Task<StartingBalance[]> GetFinancialYearStartingBalances(Guid financialYearId);
    }
}
