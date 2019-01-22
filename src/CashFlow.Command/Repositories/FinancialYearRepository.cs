using System;
using System.Linq;
using System.Threading.Tasks;
using CashFlow.Command.Abstractions.Exceptions;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CashFlow.Command.Repositories
{
    internal interface IFinancialYearRepository
    {
        Task AddFinancialYear(Guid id, string name);
        Task ActivateFinancialYear(Guid id);
    }

    internal sealed class FinancialYearRepository : IFinancialYearRepository
    {
        private readonly IDataContext _dataContext;

        public FinancialYearRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddFinancialYear(Guid id, string name)
        {
            await _dataContext.FinancialYears.AddAsync(new FinancialYear
            {
                Id = id,
                Name = name,
                DateCreated = DateTimeOffset.UtcNow
            });
            await _dataContext.SaveChangesAsync();
        }

        public async Task ActivateFinancialYear(Guid id)
        {
            using (IDbContextTransaction transaction = await _dataContext.Database.BeginTransactionAsync())
            {
                try
                {
                    FinancialYear financialYearToActivate = await _dataContext.FinancialYears.Where(x => x.Id == id).FirstOrDefaultAsync();
                    if (financialYearToActivate == null)
                        throw new FinancialYearNotFoundException(id);
                    financialYearToActivate.IsActive = true;

                    FinancialYear[] financialYearsToDeactivate = await _dataContext.FinancialYears.Where(x => x.Id != id).ToArrayAsync();
                    foreach (FinancialYear financialYearToDeactivate in financialYearsToDeactivate)
                        financialYearToDeactivate.IsActive = false;

                    await _dataContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
