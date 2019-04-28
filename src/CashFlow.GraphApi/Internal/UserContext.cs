using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.Conventions;
using GraphQL.DataLoader;

namespace CashFlow.GraphApi
{
    [ExcludeFromCodeCoverage]
    internal sealed class UserContext : IUserContext, IDataLoaderContextProvider
    {
        private readonly DataLoaderContext _dataLoaderContext;

        public UserContext(DataLoaderContext dataLoaderContext)
        {
            _dataLoaderContext = dataLoaderContext;
        }

        public async Task FetchData(CancellationToken token)
        {
            await _dataLoaderContext.DispatchAllAsync(token);
        }
    }
}
