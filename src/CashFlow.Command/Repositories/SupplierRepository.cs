using System;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Command.Repositories
{
    internal interface ISupplierRepository
    {
        Task AddSupplier(Guid id, string name, string contactInfo);
        Task RenameSupplier(Guid id, string name);
        Task UpdateSupplierContactInfo(Guid id, string contactInfo);
        Task RemoveSupplier(Guid id);
    }

    internal sealed class SupplierRepository : ISupplierRepository
    {
        private readonly IDataContext _dataContext;

        public SupplierRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddSupplier(Guid id, string name, string contactInfo)
        {
            await _dataContext.Suppliers.AddAsync(new Supplier
            {
                Id = id,
                Name = name,
                ContactInfo = contactInfo,
                DateCreated = DateTimeOffset.UtcNow
            });
            await _dataContext.SaveChangesAsync();
        }

        public async Task RenameSupplier(Guid id, string name)
        {
            Supplier supplier = await _dataContext.Suppliers.FirstAsync(x => x.Id == id);
            supplier.Name = name;
            supplier.DateModified = DateTimeOffset.UtcNow;
            _dataContext.Suppliers.Update(supplier);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateSupplierContactInfo(Guid id, string contactInfo)
        {
            Supplier supplier = await _dataContext.Suppliers.FirstAsync(x => x.Id == id);
            supplier.ContactInfo = contactInfo;
            supplier.DateModified = DateTimeOffset.UtcNow;
            _dataContext.Suppliers.Update(supplier);
            await _dataContext.SaveChangesAsync();
        }

        public async Task RemoveSupplier(Guid id)
        {
            var supplier = new Supplier { Id = id };
            _dataContext.Suppliers.Attach(supplier);
            _dataContext.Suppliers.Remove(supplier);
            await _dataContext.SaveChangesAsync();
        }
    }
}
