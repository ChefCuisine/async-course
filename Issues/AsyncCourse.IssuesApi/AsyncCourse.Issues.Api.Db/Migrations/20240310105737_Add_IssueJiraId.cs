using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsyncCourse.Issues.Api.Db.Migrations
{
    public partial class Add_IssueJiraId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "jira_id",
                table: "issues",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "jira_id",
                table: "issues");
        }
    }
}
