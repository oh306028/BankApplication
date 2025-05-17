using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApplication.Data.Migrations
{
    public partial class adminPasswordChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PublicId" },
                values: new object[] { "AQAAAAIAAYagAAAAELBQ1+PDtUJPc31emYY8MdjP9zirLtLeWppq20jtNO5ZgkKvg0sCJ5elSbDPJULweA==", new Guid("895cb376-0195-44d2-b811-0e62aa144acf") });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("05430f87-589c-4814-bf1b-e22b3aa65054"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PublicId" },
                values: new object[] { "AQAAAAIAAYagAAAAEMRcgL0snx6ZAHU5PZRGzbn6a6nIR4TSfiKHhx6eEJk1FZ1FZKnZCJh0JGXYfWzKnA==", new Guid("fd696425-40d8-468c-9d2f-a36e84d421c4") });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: new Guid("bc70dbca-2986-4d68-a187-d1068fd0e245"));
        }
    }
}
