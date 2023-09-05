using Microsoft.EntityFrameworkCore.Migrations;

namespace MMABackend.Migrations
{
    public partial class init12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectivePurchaserCollectiveSoldProduct");

            migrationBuilder.DropIndex(
                name: "IX_CollectivePurchasers_BuyerId",
                table: "CollectivePurchasers");

            migrationBuilder.CreateTable(
                name: "ShopPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShopId = table.Column<int>(type: "INTEGER", nullable: false),
                    Latitude = table.Column<decimal>(type: "Decimal(8,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "Decimal(9,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopPoints_ShopLocationDetails_ShopId",
                        column: x => x.ShopId,
                        principalTable: "ShopLocationDetails",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectivePurchasers_BuyerId_CollectiveSoldProductId",
                table: "CollectivePurchasers",
                columns: new[] { "BuyerId", "CollectiveSoldProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectivePurchasers_CollectiveSoldProductId",
                table: "CollectivePurchasers",
                column: "CollectiveSoldProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPoints_ShopId_Latitude_Longitude",
                table: "ShopPoints",
                columns: new[] { "ShopId", "Latitude", "Longitude" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectivePurchasers_CollectiveSoldProducts_CollectiveSoldProductId",
                table: "CollectivePurchasers",
                column: "CollectiveSoldProductId",
                principalTable: "CollectiveSoldProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectivePurchasers_CollectiveSoldProducts_CollectiveSoldProductId",
                table: "CollectivePurchasers");

            migrationBuilder.DropTable(
                name: "ShopPoints");

            migrationBuilder.DropIndex(
                name: "IX_CollectivePurchasers_BuyerId_CollectiveSoldProductId",
                table: "CollectivePurchasers");

            migrationBuilder.DropIndex(
                name: "IX_CollectivePurchasers_CollectiveSoldProductId",
                table: "CollectivePurchasers");

            migrationBuilder.CreateTable(
                name: "CollectivePurchaserCollectiveSoldProduct",
                columns: table => new
                {
                    CollectivePurchasersId = table.Column<int>(type: "INTEGER", nullable: false),
                    CollectiveSoldProductsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectivePurchaserCollectiveSoldProduct", x => new { x.CollectivePurchasersId, x.CollectiveSoldProductsId });
                    table.ForeignKey(
                        name: "FK_CollectivePurchaserCollectiveSoldProduct_CollectivePurchasers_CollectivePurchasersId",
                        column: x => x.CollectivePurchasersId,
                        principalTable: "CollectivePurchasers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectivePurchaserCollectiveSoldProduct_CollectiveSoldProducts_CollectiveSoldProductsId",
                        column: x => x.CollectiveSoldProductsId,
                        principalTable: "CollectiveSoldProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectivePurchasers_BuyerId",
                table: "CollectivePurchasers",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectivePurchaserCollectiveSoldProduct_CollectiveSoldProductsId",
                table: "CollectivePurchaserCollectiveSoldProduct",
                column: "CollectiveSoldProductsId");
        }
    }
}
