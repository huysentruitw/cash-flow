using System;
using System.ComponentModel.DataAnnotations;

namespace CashFlow.Persistence.Entities
{
    public sealed class Supplier
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }

        public string ContactInfo { get; set; }

        [Required]
        public DateTimeOffset DateCreated { get; set; }

        public DateTimeOffset? DateModified { get; set; }
    }
}
