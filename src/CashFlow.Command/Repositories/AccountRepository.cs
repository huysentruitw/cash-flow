using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Entities;
using CashFlow.Enums;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Command.Repositories
{
    internal interface IAccountRepository
    {
        Task AddAccount(Guid id, string name, AccountType type);
        Task RenameAccount(Guid id, string name);
        Task ChangeAccountType(Guid id, AccountType type);
        Task RemoveAccount(Guid id);
        Task SetupAccountBalancesForNewFinancialYear(Guid closingFinancialYear, Guid newFinancialYearId);
    }

    internal sealed class AccountRepository : IAccountRepository
    {
        private readonly IDataContext _dataContext;

        public AccountRepository(IDataContext dataContext)
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

        public async Task SetupAccountBalancesForNewFinancialYear(Guid closingFinancialYear, Guid newFinancialYearId)
        {
            Dictionary<Guid, long> startingBalances = await _dataContext.StartingBalances
                .AsNoTracking()
                .Where(x => x.FinancialYearId == closingFinancialYear)
                .ToDictionaryAsync(x => x.AccountId, x => x.StartingBalanceInCents);

            var balances = _dataContext.Transactions
                .AsNoTracking()
                .Where(x => x.FinancialYearId == closingFinancialYear)
                .ToArray()
                .GroupBy(x => x.AccountId)
                .ToDictionary(group => group.Key, group => group.Sum(x => x.AmountInCents));

            foreach (var kvp in balances)
            {
                Guid accountId = kvp.Key;
                startingBalances.TryGetValue(accountId, out long startingBalance);
                startingBalance += kvp.Value;
                await _dataContext.StartingBalances.AddAsync(new StartingBalance
                {
                    AccountId = accountId,
                    FinancialYearId = newFinancialYearId,
                    StartingBalanceInCents = startingBalance,
                });
            }

            await _dataContext.SaveChangesAsync();
        }
    }
}
