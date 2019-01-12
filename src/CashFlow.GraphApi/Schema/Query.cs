using System.Threading.Tasks;
using AutoMapper;
using GraphQL.Conventions;

namespace CashFlow.GraphApi.Schema
{
    internal sealed class Query
    {
        private static IMapper _mapper;

        public Query(OutputTypesMapperResolver mapperResolver)
        {
            _mapper = mapperResolver();
        }
    }
}
