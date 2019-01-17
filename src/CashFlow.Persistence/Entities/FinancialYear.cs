using System;
using System.ComponentModel.DataAnnotations;

namespace CashFlow.Persistence.Entities
{
    public sealed class FinancialYear
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public DateTimeOffset DateCreated { get; set; }
    }
}
