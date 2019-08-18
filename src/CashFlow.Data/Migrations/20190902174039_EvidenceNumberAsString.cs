using Microsoft.EntityFrameworkCore.Migrations;

namespace CashFlow.Data.Migrations
{
    public partial class EvidenceNumberAsString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_FinancialYearId_EvidenceNumber",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "EvidenceNumber",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_EvidenceNumber",
                table: "Transactions",
                column: "EvidenceNumber",
                unique: true,
                filter: "[EvidenceNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_EvidenceNumber",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "EvidenceNumber",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FinancialYearId_EvidenceNumber",
                table: "Transactions",
                columns: new[] { "FinancialYearId", "EvidenceNumber" },
                unique: true,
                filter: "[EvidenceNumber] IS NOT NULL");
        }
    }
}
