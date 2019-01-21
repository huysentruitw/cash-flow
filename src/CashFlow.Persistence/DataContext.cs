using System;
using CashFlow.Enums;
using CashFlow.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateCreated = DateTimeOffset.Parse("2019-01-21 20:00:00");
            modelBuilder.Entity<Code>().HasData(new[]
            {
                new Code { Name = "6000 aankopen", DateCreated = dateCreated },
                new Code { Name = "6100 diensten en diverse goederen", DateCreated = dateCreated },
                new Code { Name = "6560 bankkosten", DateCreated = dateCreated },
                new Code { Name = "6600 uitzonderlijke kosten", DateCreated = dateCreated },
                new Code { Name = "7000 verkopen", DateCreated = dateCreated },
                new Code { Name = "7400 diverse opbrengsten", DateCreated = dateCreated },
                new Code { Name = "7510 ontvangen bankintresten", DateCreated = dateCreated }
            });

            modelBuilder.Entity<Account>().HasData(new[]
            {
                new Account { Id = Guid.Parse("9de4b69a-79c4-4613-b2c6-c2145979a158"), Name = "Cash", Type = AccountType.CashAccount, DateCreated = dateCreated },
                new Account { Id = Guid.Parse("4612dc6d-708f-441f-bd29-50d955221d88"), Name = "Zicht", Type = AccountType.CurrentAccount, DateCreated = dateCreated },
                new Account { Id = Guid.Parse("6fa8f317-11bc-40c5-8c3b-c5895cf5e9f4"), Name = "Spaar", Type = AccountType.SavingsAccount, DateCreated = dateCreated },
            });
        }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Code> Codes { get; set; }

        public virtual DbSet<FinancialYear> FinancialYears { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }
    }
}
