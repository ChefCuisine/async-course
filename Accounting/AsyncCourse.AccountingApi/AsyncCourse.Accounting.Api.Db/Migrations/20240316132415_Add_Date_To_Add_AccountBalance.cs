using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsyncCourse.Accounting.Api.Db.Migrations
{
    public partial class Add_Date_To_Add_AccountBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_account-balances",
                table: "account-balances");

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "account-balances",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "AccountDateKey",
                table: "account-balances",
                columns: new[] { "account_id", "date" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "AccountDateKey",
                table: "account-balances");

            migrationBuilder.DropColumn(
                name: "date",
                table: "account-balances");

            migrationBuilder.AddPrimaryKey(
                name: "PK_account-balances",
                table: "account-balances",
                column: "account_id");
        }
    }
}
