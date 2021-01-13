namespace CashFlow.GraphApi.Schema
{
    public sealed class CodeBalance
    {
        public string Name { get; set; }

        public int TotalExpenseInCents { get; set; }

        public int TotalIncomeInCents { get; set; }

        public int BalanceInCents => TotalIncomeInCents - TotalExpenseInCents;
    }
}
