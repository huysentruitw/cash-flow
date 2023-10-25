using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashFlow.Data.Migrations
{
    public partial class AddindexforTransactionCodeDateAssigned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TransactionCodes_DateAssigned",
                table: "TransactionCodes",
                column: "DateAssigned");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TransactionCodes_DateAssigned",
                table: "TransactionCodes");
        }
    }
}
