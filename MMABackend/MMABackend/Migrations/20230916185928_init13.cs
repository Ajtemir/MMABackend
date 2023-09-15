using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MMABackend.Migrations
{
    public partial class init13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shops_UserId",
                table: "Shops");

            migrationBuilder.CreateTable(
                name: "AuctionProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StartPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuctionProductUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSubmitted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AuctionProductId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionProductUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionProductUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuctionProductUsers_AuctionProducts_AuctionProductId",
                        column: x => x.AuctionProductId,
                        principalTable: "AuctionProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuctionProductUsers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shops_UserId",
                table: "Shops",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuctionProducts_IsActive_ProductId",
                table: "AuctionProducts",
                columns: new[] { "IsActive", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuctionProducts_ProductId",
                table: "AuctionProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionProductUsers_AuctionProductId",
                table: "AuctionProductUsers",
                column: "AuctionProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionProductUsers_ProductId",
                table: "AuctionProductUsers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionProductUsers_UserId",
                table: "AuctionProductUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionProductUsers");

            migrationBuilder.DropTable(
                name: "AuctionProducts");

            migrationBuilder.DropIndex(
                name: "IX_Shops_UserId",
                table: "Shops");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_UserId",
                table: "Shops",
                column: "UserId");
        }
    }
}
