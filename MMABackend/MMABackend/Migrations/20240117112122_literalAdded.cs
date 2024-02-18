using Microsoft.EntityFrameworkCore.Migrations;

namespace MMABackend.Migrations
{
    public partial class literalAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyKeys_Categories_CategoryId",
                table: "PropertyKeys");

            migrationBuilder.DropIndex(
                name: "IX_PropertyKeys_CategoryId",
                table: "PropertyKeys");

            migrationBuilder.DropColumn(
                name: "IsMultiple",
                table: "PropertyKeys");

            migrationBuilder.AddColumn<bool>(
                name: "IsMultipleOrLiteralDefault",
                table: "PropertyKeys",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LiteralValue",
                table: "ProductProperties",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PropertyKeyId",
                table: "ProductProperties",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryPropertyKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    PropertyKeyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPropertyKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryPropertyKeys_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPropertyKeys_PropertyKeys_PropertyKeyId",
                        column: x => x.PropertyKeyId,
                        principalTable: "PropertyKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProperties_PropertyKeyId",
                table: "ProductProperties",
                column: "PropertyKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPropertyKeys_CategoryId",
                table: "CategoryPropertyKeys",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPropertyKeys_PropertyKeyId",
                table: "CategoryPropertyKeys",
                column: "PropertyKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProperties_PropertyKeys_PropertyKeyId",
                table: "ProductProperties",
                column: "PropertyKeyId",
                principalTable: "PropertyKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperties_PropertyKeys_PropertyKeyId",
                table: "ProductProperties");

            migrationBuilder.DropTable(
                name: "CategoryPropertyKeys");

            migrationBuilder.DropIndex(
                name: "IX_ProductProperties_PropertyKeyId",
                table: "ProductProperties");

            migrationBuilder.DropColumn(
                name: "IsMultipleOrLiteralDefault",
                table: "PropertyKeys");

            migrationBuilder.DropColumn(
                name: "LiteralValue",
                table: "ProductProperties");

            migrationBuilder.DropColumn(
                name: "PropertyKeyId",
                table: "ProductProperties");

            migrationBuilder.AddColumn<bool>(
                name: "IsMultiple",
                table: "PropertyKeys",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyKeys_CategoryId",
                table: "PropertyKeys",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyKeys_Categories_CategoryId",
                table: "PropertyKeys",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
