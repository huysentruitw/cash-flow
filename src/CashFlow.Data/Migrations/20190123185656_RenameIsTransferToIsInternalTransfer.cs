using Microsoft.EntityFrameworkCore.Migrations;

namespace CashFlow.Data.Migrations
{
    public partial class RenameIsTransferToIsInternalTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsTransfer",
                table: "Transactions",
                newName: "IsInternalTransfer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsInternalTransfer",
                table: "Transactions",
                newName: "IsTransfer");
        }
    }
}
