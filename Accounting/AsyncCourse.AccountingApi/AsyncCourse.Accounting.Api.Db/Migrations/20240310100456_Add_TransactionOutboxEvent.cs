using System;
using AsyncCourse.Accounting.Api.Models.Transactions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsyncCourse.Accounting.Api.Db.Migrations
{
    public partial class Add_TransactionOutboxEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transaction-events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "date", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    transaction_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction-events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "date", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    issue_info = table.Column<IssueTransactionInfo>(type: "jsonb", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transaction-events");

            migrationBuilder.DropTable(
                name: "transactions");
        }
    }
}
