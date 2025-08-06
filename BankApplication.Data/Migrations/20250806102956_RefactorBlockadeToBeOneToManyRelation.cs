using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApplication.Data.Migrations
{
    public partial class RefactorBlockadeToBeOneToManyRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankAccountBlockRequests_BankAccountId",
                table: "BankAccountBlockRequests");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("b6d8ad62-04e8-4998-beb0-68eb1576efb6"));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("78924c44-1bb3-4f6d-aa11-b9f99740a942"));

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBlockRequests_BankAccountId",
                table: "BankAccountBlockRequests",
                column: "BankAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankAccountBlockRequests_BankAccountId",
                table: "BankAccountBlockRequests");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("b6c4897f-95ac-404a-8b40-691a20cb0690"));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("c4676ab7-f087-494c-8dc7-61a11816f631"));

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBlockRequests_BankAccountId",
                table: "BankAccountBlockRequests",
                column: "BankAccountId",
                unique: true);
        }
    }
}
