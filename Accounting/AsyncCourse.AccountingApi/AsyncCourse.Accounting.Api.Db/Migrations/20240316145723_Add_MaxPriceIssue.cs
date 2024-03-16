using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsyncCourse.Accounting.Api.Db.Migrations
{
    public partial class Add_MaxPriceIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "max-price-issues",
                columns: table => new
                {
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    transaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    issue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_max-price-issues", x => x.date);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "max-price-issues");
        }
    }
}
