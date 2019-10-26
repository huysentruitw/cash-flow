using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Entities;
using CashFlow.Query.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Query.Repositories
{
    internal sealed class FinancialYearRepository : IFinancialYearRepository
    {
        private readonly IDataContext _dataContext;

        public FinancialYearRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<FinancialYear> GetFinancialYear(Guid financialYearId)
            => await _dataContext.FinancialYears
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == financialYearId);

        public async Task<FinancialYear[]> GetFinancialYears()
            => await _dataContext.FinancialYears.AsNoTracking().OrderBy(x => x.Name).ToArrayAsync();

        public async Task<IDictionary<Guid, FinancialYear>> GetFinancialYearsInBatch(IEnumerable<Guid> financialYearIds)
            => await _dataContext.FinancialYears.AsNoTracking().Where(x => financialYearIds.Contains(x.Id)).ToDictionaryAsync(x => x.Id);

        public async Task<StartingBalance[]> GetFinancialYearStartingBalances(Guid financialYearId)
            => await _dataContext.Accounts
                .AsNoTracking()
                .Select(account =>
                    _dataContext.StartingBalances.AsNoTracking().FirstOrDefault(balance => balance.AccountId == account.Id && balance.FinancialYearId == financialYearId)
                    ?? new StartingBalance
                    {
                        AccountId = account.Id,
                        FinancialYearId = financialYearId,
                        StartingBalanceInCents = 0
                    })
                .ToArrayAsync();
    }
}
