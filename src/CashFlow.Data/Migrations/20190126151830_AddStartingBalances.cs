using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CashFlow.Data.Migrations
{
    public partial class AddStartingBalances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StartingBalances",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(nullable: false),
                    FinancialYearId = table.Column<Guid>(nullable: false),
                    StartingBalanceInCents = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartingBalances", x => new { x.FinancialYearId, x.AccountId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StartingBalances");
        }
    }
}
