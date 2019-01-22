using System;
using CashFlow.Data.Abstractions.Models;
using CashFlow.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Data.EntityTypeConfigurations
{
    internal sealed class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();

            var dateCreated = DateTimeOffset.Parse("2019-01-21 20:00:00");
            builder.HasData(new[]
            {
                new Account { Id = Guid.Parse("9de4b69a-79c4-4613-b2c6-c2145979a158"), Name = "Cash", Type = AccountType.CashAccount, DateCreated = dateCreated },
                new Account { Id = Guid.Parse("4612dc6d-708f-441f-bd29-50d955221d88"), Name = "Zicht", Type = AccountType.CurrentAccount, DateCreated = dateCreated },
                new Account { Id = Guid.Parse("6fa8f317-11bc-40c5-8c3b-c5895cf5e9f4"), Name = "Spaar", Type = AccountType.SavingsAccount, DateCreated = dateCreated },
            });
        }
    }
}
