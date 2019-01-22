using System;
using CashFlow.Data.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Data.EntityTypeConfigurations
{
    internal sealed class CodeEntityTypeConfiguration : IEntityTypeConfiguration<Code>
    {
        public void Configure(EntityTypeBuilder<Code> builder)
        {
            builder.HasKey(x => x.Name);
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.DateCreated).IsRequired();

            var dateCreated = DateTimeOffset.Parse("2019-01-21 20:00:00");
            builder.HasData(new[]
            {
                new Code { Name = "6000 aankopen", DateCreated = dateCreated },
                new Code { Name = "6100 diensten en diverse goederen", DateCreated = dateCreated },
                new Code { Name = "6560 bankkosten", DateCreated = dateCreated },
                new Code { Name = "6600 uitzonderlijke kosten", DateCreated = dateCreated },
                new Code { Name = "7000 verkopen", DateCreated = dateCreated },
                new Code { Name = "7400 diverse opbrengsten", DateCreated = dateCreated },
                new Code { Name = "7510 ontvangen bankintresten", DateCreated = dateCreated }
            });
        }
    }
}
