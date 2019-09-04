using System;
using System.Threading.Tasks;
using CashFlow.Query.Abstractions.Models;

namespace CashFlow.Query.Abstractions.Repositories
{
    public interface ICodeBalanceRepository
    {
        Task<CodeBalance[]> GetCodeBalances(Guid financialYearId);
    }
}
