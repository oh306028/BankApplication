using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApplication.Data.Migrations
{
    public partial class addedClientCodeToAdminAcc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "ClientCode", "PublicId" },
                values: new object[] { "UniqueCode42553211", new Guid("ef488345-45f7-4345-ba5c-3c977ddd7a90") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("cfd29c04-e495-4211-8a1e-da25818d96bb"));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClientCode", "PublicId" },
                values: new object[] { null, new Guid("4e10a45b-64a3-49ed-a768-46d045ceb7aa") });
        }
    }
}
