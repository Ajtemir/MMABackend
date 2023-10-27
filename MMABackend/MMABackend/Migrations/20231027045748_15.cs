using Microsoft.EntityFrameworkCore.Migrations;

namespace MMABackend.Migrations
{
    public partial class _15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuctionProducts_IsActive_ProductId",
                table: "AuctionProducts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AuctionProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AuctionProducts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuctionProducts_IsActive_ProductId",
                table: "AuctionProducts",
                columns: new[] { "IsActive", "ProductId" },
                unique: true);
        }
    }
}
