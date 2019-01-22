using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CashFlow.Data.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("9de4b69a-79c4-4613-b2c6-c2145979a158"), new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), null, "Cash", 1 },
                    { new Guid("4612dc6d-708f-441f-bd29-50d955221d88"), new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), null, "Zicht", 2 },
                    { new Guid("6fa8f317-11bc-40c5-8c3b-c5895cf5e9f4"), new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), null, "Spaar", 3 }
                });

            migrationBuilder.InsertData(
                table: "Codes",
                columns: new[] { "Name", "DateCreated" },
                values: new object[,]
                {
                    { "6000 aankopen", new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) },
                    { "6100 diensten en diverse goederen", new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) },
                    { "6560 bankkosten", new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) },
                    { "6600 uitzonderlijke kosten", new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) },
                    { "7000 verkopen", new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) },
                    { "7400 diverse opbrengsten", new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) },
                    { "7510 ontvangen bankintresten", new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("4612dc6d-708f-441f-bd29-50d955221d88"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("6fa8f317-11bc-40c5-8c3b-c5895cf5e9f4"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("9de4b69a-79c4-4613-b2c6-c2145979a158"));

            migrationBuilder.DeleteData(
                table: "Codes",
                keyColumn: "Name",
                keyValue: "6000 aankopen");

            migrationBuilder.DeleteData(
                table: "Codes",
                keyColumn: "Name",
                keyValue: "6100 diensten en diverse goederen");

            migrationBuilder.DeleteData(
                table: "Codes",
                keyColumn: "Name",
                keyValue: "6560 bankkosten");

            migrationBuilder.DeleteData(
                table: "Codes",
                keyColumn: "Name",
                keyValue: "6600 uitzonderlijke kosten");

            migrationBuilder.DeleteData(
                table: "Codes",
                keyColumn: "Name",
                keyValue: "7000 verkopen");

            migrationBuilder.DeleteData(
                table: "Codes",
                keyColumn: "Name",
                keyValue: "7400 diverse opbrengsten");

            migrationBuilder.DeleteData(
                table: "Codes",
                keyColumn: "Name",
                keyValue: "7510 ontvangen bankintresten");
        }
    }
}
