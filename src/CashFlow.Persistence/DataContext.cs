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

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Code> Codes { get; set; }

        public virtual DbSet<FinancialYear> FinancialYears { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }
    }
}
