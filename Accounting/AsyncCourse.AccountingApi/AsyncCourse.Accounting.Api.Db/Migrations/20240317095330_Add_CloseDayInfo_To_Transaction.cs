using AsyncCourse.Accounting.Api.Models.Transactions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsyncCourse.Accounting.Api.Db.Migrations
{
    public partial class Add_CloseDayInfo_To_Transaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ClosedDayInfo>(
                name: "closed_day_info",
                table: "transactions",
                type: "jsonb",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "closed_day_info",
                table: "transactions");
        }
    }
}
