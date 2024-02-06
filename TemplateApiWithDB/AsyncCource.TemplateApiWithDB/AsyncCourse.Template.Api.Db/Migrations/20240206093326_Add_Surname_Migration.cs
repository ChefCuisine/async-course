using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsyncCourse.Template.Api.Db.Migrations
{
    public partial class Add_Surname_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "surname",
                table: "template_domain_models",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "surname",
                table: "template_domain_models");
        }
    }
}
