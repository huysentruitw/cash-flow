using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CashFlow.Query.Abstractions.Repositories;
using GraphQL.Conventions;
using GraphQL.DataLoader;
using Entities = CashFlow.Data.Abstractions.Entities;

namespace CashFlow.GraphApi.Schema
{
    internal sealed class Transaction
    {
        public Guid Id { get; set; }

        public string EvidenceNumber { get; set; }

        [Ignore]
        public Guid FinancialYearId { get; set; }

        public DateTimeOffset TransactionDate { get; set; }

        public Guid AccountId { get; set; }

        [Ignore]
        public Guid? SupplierId { get; set; }

        public long AmountInCents { get; set; }

        public bool IsInternalTransfer { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public TransactionCode[] Codes { get; set; }

        public async Task<FinancialYear> FinancialYear(
            [Inject] DataLoaderContext dataLoaderContext,
            [Inject] OutputTypesMapperResolver mapperResolver,
            [Inject] IFinancialYearRepository repository)
        {
            Entities.FinancialYear financialYear = await dataLoaderContext
                .GetOrAddBatchLoader<Guid, Entities.FinancialYear>(nameof(repository.GetFinancialYearsInBatch), repository.GetFinancialYearsInBatch)
                .LoadAsync(FinancialYearId);
            return mapperResolver().Map<FinancialYear>(financialYear);
        }

        public async Task<Supplier> Supplier(
            [Inject] DataLoaderContext dataLoaderContext,
            [Inject] OutputTypesMapperResolver mapperResolver,
            [Inject] ISupplierRepository repository)
        {
            if (!SupplierId.HasValue)
                return null;

            Entities.Supplier supplier = await dataLoaderContext
                .GetOrAddBatchLoader<Guid, Entities.Supplier>(nameof(repository.GetSuppliersInBatch), repository.GetSuppliersInBatch)
                .LoadAsync(SupplierId.Value);
            return mapperResolver().Map<Supplier>(supplier);
        }
    }
}
