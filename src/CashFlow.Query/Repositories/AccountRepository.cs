using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CashFlow.Persistence;
using CashFlow.Query.Abstractions.Models;
using CashFlow.Query.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Query.Repositories
{
    internal sealed class AccountRepository : IAccountRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public AccountRepository(DataContext dataContext, EntityMapperResolver mapperResolver)
        {
            _dataContext = dataContext;
            _mapper = mapperResolver();
        }

        public async Task<Account[]> GetAccounts()
            => _mapper.Map<Account[]>(await _dataContext.Accounts.AsNoTracking().OrderBy(x => x.Name).ToArrayAsync());
    }
}
