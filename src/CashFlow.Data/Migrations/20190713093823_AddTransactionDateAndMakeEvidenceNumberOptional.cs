using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CashFlow.Data.Migrations
{
    public partial class AddTransactionDateAndMakeEvidenceNumberOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_FinancialYearId_EvidenceNumber",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "EvidenceNumber",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "TransactionDate",
                table: "Transactions",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionDate",
                table: "Transactions",
                column: "TransactionDate");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FinancialYearId_EvidenceNumber",
                table: "Transactions",
                columns: new[] { "FinancialYearId", "EvidenceNumber" },
                unique: true,
                filter: "[EvidenceNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionDate",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FinancialYearId_EvidenceNumber",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "EvidenceNumber",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FinancialYearId_EvidenceNumber",
                table: "Transactions",
                columns: new[] { "FinancialYearId", "EvidenceNumber" },
                unique: true);
        }
    }
}
