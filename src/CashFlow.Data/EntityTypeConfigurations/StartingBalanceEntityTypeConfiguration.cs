using CashFlow.Data.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Data.EntityTypeConfigurations
{
    internal sealed class StartingBalanceEntityTypeConfiguration : IEntityTypeConfiguration<StartingBalance>
    {
        public void Configure(EntityTypeBuilder<StartingBalance> builder)
        {
            builder.HasKey(x => new { x.FinancialYearId, x.AccountId });
        }
    }
}
