using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions.Models;

namespace CashFlow.Query.Abstractions.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction[]> GetTransactions(Guid financialYearId);
        Task<ILookup<Guid, TransactionCode>> GetTransactionCodesInBatch(IEnumerable<Guid> transactionIds);
    }
}
