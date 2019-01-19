using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CashFlow.Persistence;
using CashFlow.Query.Abstractions.Models;
using CashFlow.Query.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Query.Repositories
{
    internal sealed class FinancialYearRepository : IFinancialYearRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public FinancialYearRepository(DataContext dataContext, EntityMapperResolver mapperResolver)
        {
            _dataContext = dataContext;
            _mapper = mapperResolver();
        }

        public async Task<FinancialYear[]> GetFinancialYears()
            => _mapper.Map<FinancialYear[]>(await _dataContext.FinancialYears.AsNoTracking().OrderBy(x => x.Name).ToArrayAsync());
    }
}
