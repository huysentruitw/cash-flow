using System;
using System.Linq;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Entities;
using CashFlow.Query.Abstractions.Models;
using CashFlow.Query.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Query.Repositories
{
    internal sealed class CodeBalanceRepository : ICodeBalanceRepository
    {
        private readonly IDataContext _dataContext;

        public CodeBalanceRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<CodeBalance[]> GetCodeBalances(Guid? financialYearId)
        {
            var query = _dataContext.Transactions
                .AsNoTracking()
                .Join(
                    _dataContext.TransactionCodes,
                    transaction => transaction.Id,
                    code => code.TransactionId,
                    (transaction, code) => new { Transaction = transaction, CodeName = code.CodeName }
                );

            if (financialYearId.HasValue)
                query = query.Where(x => x.Transaction.FinancialYearId == financialYearId.Value);

            return (await query.ToArrayAsync())
                .GroupBy(
                    x => x.CodeName,
                    (codeName, rows) => new CodeBalance
                    {
                        Name = codeName,
                        TotalExpenseInCents = rows.Where(x => x.Transaction.AmountInCents < 0).Sum(x => -x.Transaction.AmountInCents),
                        TotalIncomeInCents = rows.Where(x => x.Transaction.AmountInCents > 0).Sum(x => x.Transaction.AmountInCents),
                    })
                .OrderBy(x => x.Name)
                .ToArray();
        }

        public async Task<Transaction[]> GetCodeTransactions(Guid? financialYearId, string codeName)
        {
            var query = _dataContext.Transactions
                .AsNoTracking()
                .Join(
                    _dataContext.TransactionCodes,
                    transaction => transaction.Id,
                    code => code.TransactionId,
                    (transaction, code) => new { Transaction = transaction, CodeName = code.CodeName })
                .Where(x => x.CodeName == codeName);

            if (financialYearId.HasValue)
                query = query.Where(x => x.Transaction.FinancialYearId == financialYearId.Value);

            return await query
                .Select(x => x.Transaction)
                .OrderBy(x => x.TransactionDate)
                .ThenBy(x => x.DateCreated)
                .ToArrayAsync();
        }
    }
}
