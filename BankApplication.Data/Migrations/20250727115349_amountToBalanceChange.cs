using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApplication.Data.Migrations
{
    public partial class amountToBalanceChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "BankAccounts",
                newName: "Balance");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("594377f9-617f-47ab-ac20-a1b216d7ed36"));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("278b08da-a086-40cc-bef5-549a156ffba4"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "BankAccounts",
                newName: "Amount");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("27e86427-9e32-4303-8e5f-897526b07422"));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("ef488345-45f7-4345-ba5c-3c977ddd7a90"));
        }
    }
}
