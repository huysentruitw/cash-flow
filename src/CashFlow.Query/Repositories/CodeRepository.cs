using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CashFlow.Persistence;
using CashFlow.Query.Abstractions.Models;
using CashFlow.Query.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Query.Repositories
{
    internal sealed class CodeRepository : ICodeRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CodeRepository(DataContext dataContext, EntityMapperResolver mapperResolver)
        {
            _dataContext = dataContext;
            _mapper = mapperResolver();
        }

        public async Task<Code[]> GetCodes()
            => _mapper.Map<Code[]>(await _dataContext.Codes.AsNoTracking().OrderBy(x => x.Name).ToArrayAsync());
    }
}
