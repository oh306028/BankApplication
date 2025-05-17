using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApplication.Data.Migrations
{
    public partial class adminAccountAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "BirthDate", "City", "Country", "Email", "FirstName", "IsActive", "LastName", "Nationality", "Number", "PESEL", "Phone", "PostalCode", "PublicId" },
                values: new object[] { 1, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gliwice", "Polska", "admin@admin.pl", "Admin", true, "Admin", "Polski", "", "", "", "", new Guid("d562dccb-cf1c-4de8-9a30-2cc792951072") });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "ClientId", "ClosedDate", "IsBlocked", "IsDoubleAuthenticated", "IsEmployee", "Login", "PasswordHash", "PublicId", "WrongLoginAmount" },
                values: new object[] { 1, 1, null, false, false, true, "adminTab", "AQAAAAIAAYagAAAAEMRcgL0snx6ZAHU5PZRGzbn6a6nIR4TSfiKHhx6eEJk1FZ1FZKnZCJh0JGXYfWzKnA==", new Guid("15f8709a-1730-4661-91df-75e5514d3818"), 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
