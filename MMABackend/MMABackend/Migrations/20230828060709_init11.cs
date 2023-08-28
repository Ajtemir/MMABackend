using Microsoft.EntityFrameworkCore.Migrations;

namespace MMABackend.Migrations
{
    public partial class init11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectivePurchasers_AspNetUsers_BuyerId1",
                table: "CollectivePurchasers");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectivePurchasers_CollectiveSoldProducts_CollectiveSoldProductId",
                table: "CollectivePurchasers");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopLocationDetails_Shops_Id",
                table: "ShopLocationDetails");

            migrationBuilder.DropIndex(
                name: "IX_CollectiveSoldProducts_ProductId",
                table: "CollectiveSoldProducts");

            migrationBuilder.DropIndex(
                name: "IX_CollectivePurchasers_BuyerId1",
                table: "CollectivePurchasers");

            migrationBuilder.DropIndex(
                name: "IX_CollectivePurchasers_CollectiveSoldProductId",
                table: "CollectivePurchasers");

            migrationBuilder.DropColumn(
                name: "BuyerId1",
                table: "CollectivePurchasers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ShopLocationDetails",
                newName: "ShopId");

            migrationBuilder.RenameColumn(
                name: "TimeSpan",
                table: "CollectiveSoldProducts",
                newName: "EndDate");

            migrationBuilder.AlterColumn<int>(
                name: "ShopId",
                table: "ShopLocationDetails",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "MarketId",
                table: "ShopLocationDetails",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CollectiveSoldProducts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerId",
                table: "CollectivePurchasers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

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
                name: "IX_ShopLocationDetails_MarketId",
                table: "ShopLocationDetails",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShopId",
                table: "Products",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveSoldProducts_ProductId_IsActual",
                table: "CollectiveSoldProducts",
                columns: new[] { "ProductId", "IsActual" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectivePurchasers_BuyerId",
                table: "CollectivePurchasers",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectivePurchaserCollectiveSoldProduct_CollectiveSoldProductsId",
                table: "CollectivePurchaserCollectiveSoldProduct",
                column: "CollectiveSoldProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectivePurchasers_AspNetUsers_BuyerId",
                table: "CollectivePurchasers",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shops_ShopId",
                table: "Products",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopLocationDetails_Markets_MarketId",
                table: "ShopLocationDetails",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopLocationDetails_Shops_ShopId",
                table: "ShopLocationDetails",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectivePurchasers_AspNetUsers_BuyerId",
                table: "CollectivePurchasers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shops_ShopId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopLocationDetails_Markets_MarketId",
                table: "ShopLocationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopLocationDetails_Shops_ShopId",
                table: "ShopLocationDetails");

            migrationBuilder.DropTable(
                name: "CollectivePurchaserCollectiveSoldProduct");

            migrationBuilder.DropIndex(
                name: "IX_ShopLocationDetails_MarketId",
                table: "ShopLocationDetails");

            migrationBuilder.DropIndex(
                name: "IX_Products_ShopId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_CollectiveSoldProducts_ProductId_IsActual",
                table: "CollectiveSoldProducts");

            migrationBuilder.DropIndex(
                name: "IX_CollectivePurchasers_BuyerId",
                table: "CollectivePurchasers");

            migrationBuilder.DropColumn(
                name: "MarketId",
                table: "ShopLocationDetails");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CollectiveSoldProducts");

            migrationBuilder.RenameColumn(
                name: "ShopId",
                table: "ShopLocationDetails",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "CollectiveSoldProducts",
                newName: "TimeSpan");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ShopLocationDetails",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "BuyerId",
                table: "CollectivePurchasers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerId1",
                table: "CollectivePurchasers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveSoldProducts_ProductId",
                table: "CollectiveSoldProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectivePurchasers_BuyerId1",
                table: "CollectivePurchasers",
                column: "BuyerId1");

            migrationBuilder.CreateIndex(
                name: "IX_CollectivePurchasers_CollectiveSoldProductId",
                table: "CollectivePurchasers",
                column: "CollectiveSoldProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectivePurchasers_AspNetUsers_BuyerId1",
                table: "CollectivePurchasers",
                column: "BuyerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectivePurchasers_CollectiveSoldProducts_CollectiveSoldProductId",
                table: "CollectivePurchasers",
                column: "CollectiveSoldProductId",
                principalTable: "CollectiveSoldProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopLocationDetails_Shops_Id",
                table: "ShopLocationDetails",
                column: "Id",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
