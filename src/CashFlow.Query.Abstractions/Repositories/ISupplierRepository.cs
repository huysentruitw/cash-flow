using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions.Entities;

namespace CashFlow.Query.Abstractions.Repositories
{
    public interface ISupplierRepository
    {
        Task<Supplier[]> GetSuppliers();
        Task<IDictionary<Guid, Supplier>> GetSuppliersInBatch(IEnumerable<Guid> supplierIds);
    }
}
