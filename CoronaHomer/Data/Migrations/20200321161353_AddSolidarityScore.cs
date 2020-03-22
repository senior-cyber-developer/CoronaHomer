using Microsoft.EntityFrameworkCore.Migrations;

namespace CoronaHomer.Data.Migrations
{
    public partial class AddSolidarityScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SolidarityScore",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SolidarityScore",
                table: "AspNetUsers");
        }
    }
}
