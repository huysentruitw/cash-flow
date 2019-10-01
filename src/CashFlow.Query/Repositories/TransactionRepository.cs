using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Entities;
using CashFlow.Query.Abstractions.Exceptions;
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

        public async Task<string> SuggestEvidenceNumberForTransaction(Guid transactionId)
        {
            Transaction transaction = await _dataContext.Transactions
                .AsNoTracking()
                .Where(x => x.Id == transactionId)
                .FirstOrDefaultAsync()
                ?? throw new TransactionNotFoundException(transactionId);

            string latestEvidenceNumber = await _dataContext.Transactions
                .AsNoTracking()
                .Where(x => x.FinancialYearId == transaction.FinancialYearId && x.EvidenceNumber != null)
                .OrderByDescending(x => x.TransactionDate)
                .ThenByDescending(x => x.DateCreated)
                .Select(x => x.EvidenceNumber)
                .FirstOrDefaultAsync();

            if (latestEvidenceNumber == null)
                return null;

            Match match = Regex.Match(latestEvidenceNumber, @"^(?<Prefix>.*[^0-9])?(?<Number>[0-9]+)$");
            if (!match.Success)
                return null;

            string prefix = match.Groups["Prefix"].Value;
            int nextSequenceNumber = int.Parse(match.Groups["Number"].Value) + 1;
            int sequenceNumberWidth = match.Groups["Number"].Length;
            return $"{prefix}{nextSequenceNumber.ToString($"D{sequenceNumberWidth}")}";
        }
    }
}
