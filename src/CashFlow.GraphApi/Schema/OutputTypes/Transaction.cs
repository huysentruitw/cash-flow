using System;
using System.Threading;
using System.Threading.Tasks;
using CashFlow.Query.Abstractions.Repositories;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Entities = CashFlow.Data.Abstractions.Entities;

namespace CashFlow.GraphApi.Schema
{
    public sealed class Transaction
    {
        public Guid Id { get; set; }

        public string EvidenceNumber { get; set; }

        [GraphQLIgnore]
        public Guid FinancialYearId { get; set; }

        public DateTimeOffset TransactionDate { get; set; }

        public Guid AccountId { get; set; }

        [GraphQLIgnore]
        public Guid? SupplierId { get; set; }

        public long AmountInCents { get; set; }

        public bool IsInternalTransfer { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public TransactionCode[] Codes { get; set; }

        public async Task<FinancialYear> FinancialYear(
            IResolverContext context,
            [Service] OutputTypesMapperResolver mapperResolver,
            [Service] IFinancialYearRepository repository,
            CancellationToken cancellationToken)
        {
            Entities.FinancialYear financialYear = await context
                .BatchDataLoader<Guid, Entities.FinancialYear>(repository.GetFinancialYearsInBatch, "financialYearById")
                .LoadAsync(FinancialYearId, cancellationToken);
            return mapperResolver().Map<FinancialYear>(financialYear);
        }

        public async Task<Supplier> Supplier(
            IResolverContext context,
            [Service] OutputTypesMapperResolver mapperResolver,
            [Service] ISupplierRepository repository,
            CancellationToken cancellationToken)
        {
            if (!SupplierId.HasValue)
                return null;

            Entities.Supplier supplier = await context
                .BatchDataLoader<Guid, Entities.Supplier>(repository.GetSuppliersInBatch, "supplierById")
                .LoadAsync(SupplierId.Value, cancellationToken);
            return mapperResolver().Map<Supplier>(supplier);
        }
    }
}
