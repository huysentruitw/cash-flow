using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CashFlow.Persistence;
using CashFlow.Query.Abstractions.Models;
using CashFlow.Query.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Query.Repositories
{
    internal sealed class SupplierRepository : ISupplierRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public SupplierRepository(DataContext dataContext, EntityMapperResolver mapperResolver)
        {
            _dataContext = dataContext;
            _mapper = mapperResolver();
        }

        public async Task<Supplier[]> GetSuppliers()
            => _mapper.Map<Supplier[]>(await _dataContext.Suppliers.AsNoTracking().OrderBy(x => x.Name).ToArrayAsync());
    }
}
