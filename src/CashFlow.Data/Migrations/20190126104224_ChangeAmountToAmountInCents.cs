using Microsoft.EntityFrameworkCore.Migrations;

namespace CashFlow.Data.Migrations
{
    public partial class ChangeAmountToAmountInCents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transactions");

            migrationBuilder.AddColumn<long>(
                name: "AmountInCents",
                table: "Transactions",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountInCents",
                table: "Transactions");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
