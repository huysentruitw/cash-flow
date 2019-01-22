using System.Linq;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Models;
using CashFlow.Query.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Query.Repositories
{
    internal sealed class SupplierRepository : ISupplierRepository
    {
        private readonly IDataContext _dataContext;

        public SupplierRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Supplier[]> GetSuppliers()
            => await _dataContext.Suppliers.AsNoTracking().OrderBy(x => x.Name).ToArrayAsync();
    }
}
