using System;

namespace CashFlow.Data.Abstractions.Models
{
    public sealed class Code
    {
        public string Name { get; set; }

        public DateTimeOffset DateCreated { get; set; }
    }
}
