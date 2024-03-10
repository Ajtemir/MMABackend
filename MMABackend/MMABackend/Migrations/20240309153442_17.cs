using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MMABackend.Migrations
{
    public partial class _17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectivePurchasers_CollectiveSoldProducts_CollectiveSoldProductId",
                table: "CollectivePurchasers");

            migrationBuilder.DropTable(
                name: "CollectiveSoldProducts");

            migrationBuilder.RenameColumn(
                name: "LiteralValue",
                table: "ProductProperties",
                newName: "NumberValue");

            migrationBuilder.RenameColumn(
                name: "CollectiveSoldProductId",
                table: "CollectivePurchasers",
                newName: "GroupDiscountProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectivePurchasers_CollectiveSoldProductId",
                table: "CollectivePurchasers",
                newName: "IX_CollectivePurchasers_GroupDiscountProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectivePurchasers_BuyerId_CollectiveSoldProductId",
                table: "CollectivePurchasers",
                newName: "IX_CollectivePurchasers_BuyerId_GroupDiscountProductId");

            migrationBuilder.CreateTable(
                name: "GroupDiscountProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActual = table.Column<bool>(type: "INTEGER", nullable: true),
                    GroupDiscountPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    BuyerMinAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDiscountProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupDiscountProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyKeys_CategoryId",
                table: "PropertyKeys",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupDiscountProducts_ProductId_IsActual",
                table: "GroupDiscountProducts",
                columns: new[] { "ProductId", "IsActual" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectivePurchasers_GroupDiscountProducts_GroupDiscountProductId",
                table: "CollectivePurchasers",
                column: "GroupDiscountProductId",
                principalTable: "GroupDiscountProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyKeys_Categories_CategoryId",
                table: "PropertyKeys",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectivePurchasers_GroupDiscountProducts_GroupDiscountProductId",
                table: "CollectivePurchasers");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyKeys_Categories_CategoryId",
                table: "PropertyKeys");

            migrationBuilder.DropTable(
                name: "GroupDiscountProducts");

            migrationBuilder.DropIndex(
                name: "IX_PropertyKeys_CategoryId",
                table: "PropertyKeys");

            migrationBuilder.RenameColumn(
                name: "NumberValue",
                table: "ProductProperties",
                newName: "LiteralValue");

            migrationBuilder.RenameColumn(
                name: "GroupDiscountProductId",
                table: "CollectivePurchasers",
                newName: "CollectiveSoldProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectivePurchasers_GroupDiscountProductId",
                table: "CollectivePurchasers",
                newName: "IX_CollectivePurchasers_CollectiveSoldProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectivePurchasers_BuyerId_GroupDiscountProductId",
                table: "CollectivePurchasers",
                newName: "IX_CollectivePurchasers_BuyerId_CollectiveSoldProductId");

            migrationBuilder.CreateTable(
                name: "CollectiveSoldProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BuyerMinAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    CollectivePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActual = table.Column<bool>(type: "INTEGER", nullable: true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveSoldProducts_ProductId_IsActual",
                table: "CollectiveSoldProducts",
                columns: new[] { "ProductId", "IsActual" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectivePurchasers_CollectiveSoldProducts_CollectiveSoldProductId",
                table: "CollectivePurchasers",
                column: "CollectiveSoldProductId",
                principalTable: "CollectiveSoldProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
