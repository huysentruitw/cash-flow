using System;
using System.Linq;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace CashFlow.Command.Repositories
{
    internal interface ITransactionRepository
    {
        Task Add(Guid id, Guid financialYearId, Guid accountId, Guid? supplierId, decimal amount, bool isInternalTransfer, string description, string comment, string[] codeNames);
    }

    internal sealed class TransactionRepository : ITransactionRepository
    {
        private readonly IDataContext _dataContext;
        private readonly Func<DateTimeOffset> _utcNowFactory;

        public TransactionRepository(IDataContext dataContext, Func<DateTimeOffset> utcNowFactory = null)
        {
            _dataContext = dataContext;
            _utcNowFactory = utcNowFactory ?? (() => DateTimeOffset.UtcNow);
        }

        public async Task Add(Guid id, Guid financialYearId, Guid accountId, Guid? supplierId, decimal amount, bool isInternalTransfer, string description, string comment, string[] codeNames)
        {
            using (IDbContextTransaction transaction = await _dataContext.Database.BeginTransactionAsync())
            {
                try
                {
                    int evidenceNumber = 1 + _dataContext.Transactions
                        .Where(x => x.FinancialYearId == financialYearId)
                        .Select(x => x.EvidenceNumber)
                        .DefaultIfEmpty()
                        .Max();

                    DateTimeOffset utcNow = _utcNowFactory();

                    await _dataContext.Transactions.AddAsync(new Transaction
                    {
                        Id = id,
                        EvidenceNumber = evidenceNumber,
                        FinancialYearId = financialYearId,
                        AccountId = accountId,
                        SupplierId = supplierId,
                        DateCreated = utcNow,
                        Amount = amount,
                        IsInternalTransfer = isInternalTransfer,
                        Description = description,
                        Comment = comment
                    });

                    foreach (string codeName in codeNames)
                    {
                        await _dataContext.TransactionCodes.AddAsync(new TransactionCode
                        {
                            TransactionId = id,
                            CodeName = codeName,
                            DateAssigned = utcNow
                        });
                    }

                    await _dataContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
