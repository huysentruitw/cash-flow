using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Models;
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
                .Where(x => x.FinancialYearId == financialYearId)
                .OrderBy(x => x.TransactionDate)
                .ThenBy(x => x.DateCreated)
                .ToArrayAsync();

        public async Task<ILookup<Guid, TransactionCode>> GetTransactionCodesInBatch(IEnumerable<Guid> transactionIds)
            => (await _dataContext.TransactionCodes
                .AsNoTracking()
                .Where(x => transactionIds.Contains(x.TransactionId))
                .ToArrayAsync()).ToLookup(x => x.TransactionId);
    }
}
