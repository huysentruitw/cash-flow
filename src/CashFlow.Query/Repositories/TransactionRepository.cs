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
    internal sealed class TransactionRepository : ITransactionRepository
    {
        private readonly IDataContext _dataContext;

        public TransactionRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Transaction[]> GetTransactions(Guid financialYearId)
            => await _dataContext.Transactions
                .AsNoTracking()
                .Include(x => x.Codes)
                .Where(x => x.FinancialYearId == financialYearId)
                .OrderBy(x => x.TransactionDate)
                .ThenBy(x => x.DateCreated)
                .ToArrayAsync();
    }
}
