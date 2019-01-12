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


    }
}
