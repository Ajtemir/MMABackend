using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MMABackend.Migrations
{
    public partial class init7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollectiveSoldProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActual = table.Column<bool>(type: "INTEGER", nullable: true),
                    CollectivePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    BuyerMinAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeSpan = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectiveSoldProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectiveSoldProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollectivePurchasers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BuyerId = table.Column<int>(type: "INTEGER", nullable: false),
                    BuyerId1 = table.Column<string>(type: "TEXT", nullable: true),
                    CollectiveSoldProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectivePurchasers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectivePurchasers_AspNetUsers_BuyerId1",
                        column: x => x.BuyerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollectivePurchasers_CollectiveSoldProducts_CollectiveSoldProductId",
                        column: x => x.CollectiveSoldProductId,
                        principalTable: "CollectiveSoldProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectivePurchasers_BuyerId1",
                table: "CollectivePurchasers",
                column: "BuyerId1");

            migrationBuilder.CreateIndex(
                name: "IX_CollectivePurchasers_CollectiveSoldProductId",
                table: "CollectivePurchasers",
                column: "CollectiveSoldProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveSoldProducts_ProductId",
                table: "CollectiveSoldProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectivePurchasers");

            migrationBuilder.DropTable(
                name: "CollectiveSoldProducts");
        }
    }
}
