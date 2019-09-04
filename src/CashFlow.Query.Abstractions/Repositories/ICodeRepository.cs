using System.Threading.Tasks;
using CashFlow.Data.Abstractions.Entities;

namespace CashFlow.Query.Abstractions.Repositories
{
    public interface ICodeRepository
    {
        Task<Code[]> GetCodes();
    }
}
