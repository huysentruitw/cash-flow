using System;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions.Entities;
using CashFlow.Query.Abstractions.Models;

namespace CashFlow.Query.Abstractions.Repositories
{
    public interface ICodeBalanceRepository
    {
        Task<CodeBalance[]> GetCodeBalances(Guid financialYearId);

        Task<Transaction[]> GetCodeTransactions(Guid financialYearId, string codeName);
    }
}
