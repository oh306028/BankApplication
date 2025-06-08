using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApplication.Data.Migrations
{
    public partial class accountToLoggigsRelationChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_loggings_AccountId",
                table: "loggings");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("749b016f-e013-4f2d-87f0-c1b54150e0fd"));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("f18803cc-dcd2-4bb5-9f00-36754ac54c51"));

            migrationBuilder.CreateIndex(
                name: "IX_loggings_AccountId",
                table: "loggings",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_loggings_AccountId",
                table: "loggings");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("895cb376-0195-44d2-b811-0e62aa144acf"));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("05430f87-589c-4814-bf1b-e22b3aa65054"));

            migrationBuilder.CreateIndex(
                name: "IX_loggings_AccountId",
                table: "loggings",
                column: "AccountId",
                unique: true);
        }
    }
}
