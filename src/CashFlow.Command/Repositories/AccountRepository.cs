using System;
using System.Threading.Tasks;
using CashFlow.Enums;
using CashFlow.Persistence;
using CashFlow.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Command.Repositories
{
    internal interface IAccountRepository
    {
        Task AddAccount(Guid id, string name, AccountType type);
        Task RenameAccount(Guid id, string name);
        Task ChangeAccountType(Guid id, AccountType type);
        Task RemoveAccount(Guid id);
    }

    internal sealed class AccountRepository : IAccountRepository
    {
        private readonly DataContext _dataContext;

        public AccountRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAccount(Guid id, string name, AccountType type)
        {
            await _dataContext.Accounts.AddAsync(new Account
            {
                Id = id,
                Name = name,
                Type = type,
                DateCreated = DateTimeOffset.UtcNow
            });
            await _dataContext.SaveChangesAsync();
        }

        public async Task RenameAccount(Guid id, string name)
        {
            Account account = await _dataContext.Accounts.FirstAsync(x => x.Id == id);
            account.Name = name;
            account.DateModified = DateTimeOffset.UtcNow;
            _dataContext.Accounts.Update(account);
            await _dataContext.SaveChangesAsync();
        }

        public async Task ChangeAccountType(Guid id, AccountType type)
        {
            Account account = await _dataContext.Accounts.FirstAsync(x => x.Id == id);
            account.Type = type;
            account.DateModified = DateTimeOffset.UtcNow;
            _dataContext.Accounts.Update(account);
            await _dataContext.SaveChangesAsync();
        }

        public async Task RemoveAccount(Guid id)
        {
            var account = new Account { Id = id };
            _dataContext.Accounts.Attach(account);
            _dataContext.Accounts.Remove(account);
            await _dataContext.SaveChangesAsync();
        }
    }
}
