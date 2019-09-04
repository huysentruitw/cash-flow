using System;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions.Entities;

namespace CashFlow.Query.Abstractions.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction[]> GetTransactions(Guid financialYearId);
    }
}
