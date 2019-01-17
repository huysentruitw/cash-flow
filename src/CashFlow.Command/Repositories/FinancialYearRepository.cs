using System;
using System.Threading.Tasks;
using CashFlow.Persistence;
using CashFlow.Persistence.Entities;

namespace CashFlow.Command.Repositories
{
    internal interface IFinancialYearRepository
    {
        Task AddFinancialYear(Guid id, string name);
    }

    internal sealed class FinancialYearRepository : IFinancialYearRepository
    {
        private readonly DataContext _dataContext;

        public FinancialYearRepository(DataContext dataContext)
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
    }
}
