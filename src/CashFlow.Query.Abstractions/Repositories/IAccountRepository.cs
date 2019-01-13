using System.Threading.Tasks;
using CashFlow.Query.Abstractions.Models;

namespace CashFlow.Query.Abstractions.Repositories
{
    public interface IAccountRepository
    {
        Task<Account[]> GetAccounts();
    }
}
