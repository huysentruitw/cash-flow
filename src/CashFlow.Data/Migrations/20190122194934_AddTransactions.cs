using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CashFlow.Data.Migrations
{
    public partial class AddTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionCodes",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(nullable: false),
                    CodeName = table.Column<string>(maxLength: 100, nullable: false),
                    DateAssigned = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCodes", x => new { x.TransactionId, x.CodeName });
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EvidenceNumber = table.Column<int>(nullable: false),
                    FinancialYearId = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    SupplierId = table.Column<Guid>(nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    IsTransfer = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    Comment = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCodes_CodeName",
                table: "TransactionCodes",
                column: "CodeName");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SupplierId",
                table: "Transactions",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FinancialYearId_EvidenceNumber",
                table: "Transactions",
                columns: new[] { "FinancialYearId", "EvidenceNumber" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionCodes");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
