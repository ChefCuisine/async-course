using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsyncCourse.Issues.Api.Db.Migrations
{
    public partial class Change_AccountId_ColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "accound_id",
                table: "issues",
                newName: "assigned_to_accound_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "assigned_to_accound_id",
                table: "issues",
                newName: "accound_id");
        }
    }
}
