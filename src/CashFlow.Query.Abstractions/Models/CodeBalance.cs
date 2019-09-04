namespace CashFlow.Query.Abstractions.Models
{
    public sealed class CodeBalance
    {
        public string Name { get; set; }

        public long TotalExpenseInCents { get; set; }

        public long TotalIncomeInCents { get; set; }
    }
}
