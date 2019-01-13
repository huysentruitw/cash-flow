using System;
using System.ComponentModel.DataAnnotations;
using CashFlow.Enums;

namespace CashFlow.Persistence.Entities
{
    public sealed class Account
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public AccountType Type { get; set; }

        [Required]
        public DateTimeOffset DateCreated { get; set; }

        public DateTimeOffset? DateModified { get; set; }
    }
}
