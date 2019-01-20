using Microsoft.EntityFrameworkCore.Migrations;

namespace CashFlow.Persistence.Migrations
{
    public partial class AddIsActiveToFinancialYear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FinancialYears",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FinancialYears");
        }
    }
}
