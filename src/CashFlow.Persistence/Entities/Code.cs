using System;
using System.ComponentModel.DataAnnotations;

namespace CashFlow.Persistence.Entities
{
    public sealed class Code
    {
        [Key, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTimeOffset DateCreated { get; set; }
    }
}
