using System.Linq;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Models;
using CashFlow.Query.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Query.Repositories
{
    internal sealed class AccountRepository : IAccountRepository
    {
        private readonly IDataContext _dataContext;

        public AccountRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Account[]> GetAccounts()
            => await _dataContext.Accounts.AsNoTracking().OrderBy(x => x.Name).ToArrayAsync();
    }
}
