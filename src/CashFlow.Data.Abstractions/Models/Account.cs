using System;
using CashFlow.Enums;

namespace CashFlow.Data.Abstractions.Models
{
    public sealed class Account
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public AccountType Type { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public DateTimeOffset? DateModified { get; set; }
    }
}
