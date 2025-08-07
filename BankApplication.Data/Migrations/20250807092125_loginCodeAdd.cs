using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApplication.Data.Migrations
{
    public partial class loginCodeAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoginCode",
                table: "loggings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("6624d839-5a36-4e50-a99a-acfcfdf7cceb"));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("5f1d4e06-6aca-4366-b36a-1c20f3bda09a"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginCode",
                table: "loggings");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("b25cff5c-ef49-4f32-b51d-af69fbe5782f"));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("1a623678-2a99-44ce-8648-50953e247a62"));
        }
    }
}
