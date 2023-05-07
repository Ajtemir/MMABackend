using Microsoft.EntityFrameworkCore.Migrations;

namespace MMABackend.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconId",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Categories",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "IconId",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
