using System.Threading;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CashFlow.Data.Abstractions
{
    public interface IDataContext
    {
        DbSet<Account> Accounts { get; set; }

        DbSet<Code> Codes { get; set; }

        DbSet<FinancialYear> FinancialYears { get; set; }

        DbSet<Supplier> Suppliers { get; set; }

        DbSet<TransactionCode> TransactionCodes { get; set; }

        DbSet<Transaction> Transactions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        DatabaseFacade Database { get; }
    }
}
