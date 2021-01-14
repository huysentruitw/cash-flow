using System.Linq;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Entities;
using CashFlow.Query.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Query.Repositories
{
    internal sealed class CodeRepository : ICodeRepository
    {
        private readonly IDataContext _dataContext;

        public CodeRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Code[]> GetCodes()
            => await _dataContext.Codes.AsNoTracking().OrderBy(x => x.Name).ToArrayAsync();

        public async Task<string[]> GetActiveCodeNames()
            => await _dataContext.Codes.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Name).Select(x => x.Name).ToArrayAsync();
    }
}
