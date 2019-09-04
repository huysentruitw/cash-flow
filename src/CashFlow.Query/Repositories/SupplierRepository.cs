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
    internal sealed class SupplierRepository : ISupplierRepository
    {
        private readonly IDataContext _dataContext;

        public SupplierRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Supplier[]> GetSuppliers()
            => await _dataContext.Suppliers.AsNoTracking().OrderBy(x => x.Name).ToArrayAsync();

        public async Task<IDictionary<Guid, Supplier>> GetSuppliersInBatch(IEnumerable<Guid> supplierIds)
            => await _dataContext.Suppliers.AsNoTracking().Where(x => supplierIds.Contains(x.Id)).ToDictionaryAsync(x => x.Id);
    }
}
