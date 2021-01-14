using System;

namespace CashFlow.Data.Abstractions.Entities
{
    public sealed class Code
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public DateTimeOffset? DateModified { get; set; }
    }
}
