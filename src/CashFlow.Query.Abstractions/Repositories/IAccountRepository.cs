using System.Threading.Tasks;
using CashFlow.Data.Abstractions.Entities;

namespace CashFlow.Query.Abstractions.Repositories
{
    public interface IAccountRepository
    {
        Task<Account[]> GetAccounts();
    }
}
