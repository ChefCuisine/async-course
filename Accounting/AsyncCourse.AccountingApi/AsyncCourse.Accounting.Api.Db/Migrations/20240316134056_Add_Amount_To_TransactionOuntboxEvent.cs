using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsyncCourse.Accounting.Api.Db.Migrations
{
    public partial class Add_Amount_To_TransactionOuntboxEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "amount",
                table: "transaction-events",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount",
                table: "transaction-events");
        }
    }
}
