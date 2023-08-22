using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MMABackend.Migrations
{
    public partial class init8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_MarketShops_MarketShopId",
                table: "Shops");

            migrationBuilder.DropTable(
                name: "MarketShops");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Shops");

            migrationBuilder.RenameColumn(
                name: "MarketShopId",
                table: "Shops",
                newName: "MarketId");

            migrationBuilder.RenameColumn(
                name: "IsSealed",
                table: "Shops",
                newName: "ShopType");

            migrationBuilder.RenameIndex(
                name: "IX_Shops_MarketShopId",
                table: "Shops",
                newName: "IX_Shops_MarketId");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Markets",
                type: "Decimal(8,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Markets",
                type: "Decimal(9,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "CollectivePurchasers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ShopLocationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Stage = table.Column<int>(type: "INTEGER", nullable: false),
                    Latitude = table.Column<decimal>(type: "Decimal(8,6)", nullable: true),
                    Longitude = table.Column<decimal>(type: "Decimal(9,6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopLocationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopLocationDetails_Shops_Id",
                        column: x => x.Id,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Markets_MarketId",
                table: "Shops",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Markets_MarketId",
                table: "Shops");

            migrationBuilder.DropTable(
                name: "ShopLocationDetails");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Markets");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Markets");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "CollectivePurchasers");

            migrationBuilder.RenameColumn(
                name: "ShopType",
                table: "Shops",
                newName: "IsSealed");

            migrationBuilder.RenameColumn(
                name: "MarketId",
                table: "Shops",
                newName: "MarketShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Shops_MarketId",
                table: "Shops",
                newName: "IX_Shops_MarketShopId");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Shops",
                type: "Decimal(8,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Shops",
                type: "Decimal(9,6)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MarketShops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MarketId = table.Column<int>(type: "INTEGER", nullable: true),
                    Stage = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketShops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketShops_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketShops_MarketId",
                table: "MarketShops",
                column: "MarketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_MarketShops_MarketShopId",
                table: "Shops",
                column: "MarketShopId",
                principalTable: "MarketShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
