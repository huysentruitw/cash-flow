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

        public Task FetchData(CancellationToken token)
        {
            _dataLoaderContext.DispatchAll(token);
            return Task.CompletedTask;
        }
    }
}
