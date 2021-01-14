using Microsoft.EntityFrameworkCore.Migrations;

namespace CashFlow.Data.Migrations
{
    public partial class AddIsActiveToCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Codes",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionCodes_Codes_CodeName",
                table: "TransactionCodes",
                column: "CodeName",
                principalTable: "Codes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionCodes_Transactions_TransactionId",
                table: "TransactionCodes",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionCodes_Codes_CodeName",
                table: "TransactionCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionCodes_Transactions_TransactionId",
                table: "TransactionCodes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Codes");
        }
    }
}
