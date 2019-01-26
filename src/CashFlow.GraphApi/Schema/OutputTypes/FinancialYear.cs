using System;
using System.Threading.Tasks;
using CashFlow.Query.Abstractions.Repositories;
using GraphQL.Conventions;

namespace CashFlow.GraphApi.Schema
{
    internal sealed class FinancialYear
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public async Task<StartingBalance[]> StartingBalances([Inject] IFinancialYearRepository repository, [Inject] OutputTypesMapperResolver mapperResolver)
            => mapperResolver().Map<StartingBalance[]>(await repository.GetFinancialYearStartingBalances(Id));
    }
}
