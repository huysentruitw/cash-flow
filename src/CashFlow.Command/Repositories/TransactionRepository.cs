using System;
using System.Linq;
using System.Threading.Tasks;
using CashFlow.Command.Abstractions.Exceptions;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CashFlow.Command.Repositories
{
    internal interface ITransactionRepository
    {
        Task Add(Guid id, Guid financialYearId, DateTimeOffset transactionDate, Guid accountId, Guid? supplierId, long amountInCents, bool isInternalTransfer, string description, string comment, string[] codeNames);
        Task<bool> RemoveLatest(Guid id);
        Task AssignCode(Guid id, string codeName);
        Task UnassignCode(Guid id, string codeName);
        Task UpdateDescription(Guid id, string description);
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

        public async Task Add(Guid id, Guid financialYearId, DateTimeOffset transactionDate, Guid accountId, Guid? supplierId, long amountInCents, bool isInternalTransfer, string description, string comment, string[] codeNames)
        {
            using (IDbContextTransaction transaction = await _dataContext.Database.BeginTransactionAsync())
            {
                try
                {
                    DateTimeOffset utcNow = _utcNowFactory();

                    if (isInternalTransfer && amountInCents > 0)
                        utcNow += TimeSpan.FromSeconds(1);

                    await _dataContext.Transactions.AddAsync(new Transaction
                    {
                        Id = id,
                        FinancialYearId = financialYearId,
                        TransactionDate = transactionDate,
                        AccountId = accountId,
                        SupplierId = supplierId,
                        DateCreated = utcNow,
                        AmountInCents = amountInCents,
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

        public async Task<bool> RemoveLatest(Guid id)
        {
            Transaction transaction = await _dataContext.Transactions.FirstOrDefaultAsync(x => x.Id == id);
            if (transaction == null)
                throw new TransactionNotFoundException(id);

            // TODO Validate if this is really the latest transaction
            // or can we allow to remove any transaction? -> Need to watch out for the sequence of evidenceNumbers

            _dataContext.Transactions.Remove(transaction);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task AssignCode(Guid id, string codeName)
        {
            using (IDbContextTransaction transaction = await _dataContext.Database.BeginTransactionAsync())
            {
                DateTimeOffset utcNow = _utcNowFactory();

                try
                {
                    if (!_dataContext.Codes.Any(x => x.Name == codeName))
                    {
                        await _dataContext.Codes.AddAsync(new Code
                        {
                            Name = codeName,
                            DateCreated = utcNow
                        });
                    }

                    await _dataContext.TransactionCodes.AddAsync(new TransactionCode
                    {
                        TransactionId = id,
                        CodeName = codeName,
                        DateAssigned = utcNow
                    });
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

        public async Task UnassignCode(Guid id, string codeName)
        {
            var code = new TransactionCode { TransactionId = id, CodeName = codeName };
            _dataContext.TransactionCodes.Attach(code);
            _dataContext.TransactionCodes.Remove(code);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateDescription(Guid id, string description)
        {
            Transaction transaction = await _dataContext.Transactions.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new TransactionNotFoundException(id);

            transaction.Description = description;
            transaction.DateModified = DateTimeOffset.UtcNow;
            await _dataContext.SaveChangesAsync();
        }
    }
}
