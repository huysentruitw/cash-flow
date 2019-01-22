using System;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Command.Repositories
{
    internal interface ICodeRepository
    {
        Task AddCode(string name);
        Task RenameCode(string originalName, string newName);
        Task RemoveCode(string name);
    }

    internal sealed class CodeRepository : ICodeRepository
    {
        private readonly IDataContext _dataContext;

        public CodeRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddCode(string name)
        {
            await _dataContext.Codes.AddAsync(new Code
            {
                Name = name,
                DateCreated = DateTimeOffset.UtcNow
            });
            await _dataContext.SaveChangesAsync();
        }

        public async Task RenameCode(string originalName, string newName)
        {
            Code originalCode = await _dataContext.Codes.FirstAsync(x => x.Name == originalName);
            _dataContext.Codes.Remove(originalCode);
            await _dataContext.Codes.AddAsync(new Code
            {
                Name = newName,
                DateCreated = originalCode.DateCreated
            });
            await _dataContext.SaveChangesAsync();
        }

        public async Task RemoveCode(string name)
        {
            var code = new Code { Name = name };
            _dataContext.Codes.Attach(code);
            _dataContext.Codes.Remove(code);
            await _dataContext.SaveChangesAsync();
        }
    }
}
