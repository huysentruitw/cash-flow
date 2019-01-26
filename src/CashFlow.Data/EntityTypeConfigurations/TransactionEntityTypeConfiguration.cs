using CashFlow.Data.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Data.EntityTypeConfigurations
{
    internal sealed class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EvidenceNumber).IsRequired();
            builder.Property(x => x.FinancialYearId).IsRequired();
            builder.HasIndex(x => new { x.FinancialYearId, x.EvidenceNumber }).IsUnique();
            builder.Property(x => x.AccountId).IsRequired();
            builder.HasIndex(x => x.AccountId);
            builder.HasIndex(x => x.SupplierId);
            builder.Property(x => x.DateCreated).IsRequired();
            builder.Property(x => x.AmountInCents).IsRequired();
            builder.Property(x => x.IsInternalTransfer).IsRequired();
            builder.Property(x => x.Description).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Comment).HasMaxLength(250);
        }
    }
}
