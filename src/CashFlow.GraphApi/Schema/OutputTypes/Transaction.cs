using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CashFlow.Query.Abstractions.Repositories;
using GraphQL.Conventions;
using GraphQL.DataLoader;
using Models = CashFlow.Data.Abstractions.Models;

namespace CashFlow.GraphApi.Schema
{
    internal sealed class Transaction
    {
        public Guid Id { get; set; }

        public int EvidenceNumber { get; set; }

        [Ignore]
        public Guid FinancialYearId { get; set; }

        public Guid AccountId { get; set; }

        public Guid? SupplierId { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public DateTimeOffset? DateModified { get; set; }

        public long AmountInCents { get; set; }

        public bool IsInternalTransfer { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public async Task<TransactionCode[]> Codes(
            [Inject] DataLoaderContext dataLoaderContext,
            [Inject] OutputTypesMapperResolver mapperResolver,
            [Inject] ITransactionRepository repository)
        {
            IEnumerable<Models.TransactionCode> codes = await dataLoaderContext
                .GetOrAddCollectionBatchLoader<Guid, Models.TransactionCode>(nameof(repository.GetTransactionCodesInBatch), repository.GetTransactionCodesInBatch)
                .LoadAsync(Id);
            return mapperResolver().Map<TransactionCode[]>(codes);
        }

        public async Task<FinancialYear> FinancialYear(
            [Inject] DataLoaderContext dataLoaderContext,
            [Inject] OutputTypesMapperResolver mapperResolver,
            [Inject] IFinancialYearRepository repository)
        {
            Models.FinancialYear financialYear = await dataLoaderContext
                .GetOrAddBatchLoader<Guid, Models.FinancialYear>(nameof(repository.GetFinancialYearsInBatch), repository.GetFinancialYearsInBatch)
                .LoadAsync(FinancialYearId);
            return mapperResolver().Map<FinancialYear>(financialYear);
        }
    }
}
