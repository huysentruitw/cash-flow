using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Data
{
    internal sealed class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Code> Codes { get; set; }

        public DbSet<FinancialYear> FinancialYears { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<TransactionCode> TransactionCodes { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
