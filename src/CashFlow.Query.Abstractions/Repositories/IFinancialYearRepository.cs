using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions.Entities;

namespace CashFlow.Query.Abstractions.Repositories
{
    public interface IFinancialYearRepository
    {
        Task<FinancialYear> GetFinancialYear(Guid financialYearId);
        Task<FinancialYear[]> GetFinancialYears();
        Task<IDictionary<Guid, FinancialYear>> GetFinancialYearsInBatch(IEnumerable<Guid> financialYearIds);
        Task<StartingBalance[]> GetFinancialYearStartingBalances(Guid financialYearId);
    }
}
