using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApplication.Data.Migrations
{
    public partial class adminIsAsOneToManyRelationwithBlockReqs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankAccountBlockRequests_AdminId",
                table: "BankAccountBlockRequests");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsDoubleAuthenticated", "PublicId" },
                values: new object[] { true, new Guid("6223c367-f858-4aad-81a9-6735278c358d") });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("c21704bd-4985-45d7-abc2-a2412d2c74ca"));

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBlockRequests_AdminId",
                table: "BankAccountBlockRequests",
                column: "AdminId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankAccountBlockRequests_AdminId",
                table: "BankAccountBlockRequests");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsDoubleAuthenticated", "PublicId" },
                values: new object[] { false, new Guid("6624d839-5a36-4e50-a99a-acfcfdf7cceb") });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("5f1d4e06-6aca-4366-b36a-1c20f3bda09a"));

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBlockRequests_AdminId",
                table: "BankAccountBlockRequests",
                column: "AdminId",
                unique: true,
                filter: "[AdminId] IS NOT NULL");
        }
    }
}
