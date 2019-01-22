using CashFlow.Data.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Data.EntityTypeConfigurations
{
    internal sealed class TransactionCodeEntityTypeConfiguration : IEntityTypeConfiguration<TransactionCode>
    {
        public void Configure(EntityTypeBuilder<TransactionCode> builder)
        {
            builder.HasKey(x => new { x.TransactionId, x.CodeName });
            builder.Property(x => x.CodeName).HasMaxLength(100);
            builder.HasIndex(x => x.CodeName);
            builder.Property(x => x.DateAssigned).IsRequired();
        }
    }
}
