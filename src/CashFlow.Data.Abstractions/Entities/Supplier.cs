using System;

namespace CashFlow.Data.Abstractions.Entities
{
    public sealed class Supplier
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ContactInfo { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public DateTimeOffset? DateModified { get; set; }
    }
}
