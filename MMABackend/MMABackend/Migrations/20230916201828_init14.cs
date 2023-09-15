using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MMABackend.Migrations
{
    public partial class init14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionProductUsers_AuctionProducts_AuctionProductId",
                table: "AuctionProductUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionProductUsers_Products_ProductId",
                table: "AuctionProductUsers");

            migrationBuilder.DropIndex(
                name: "IX_AuctionProductUsers_ProductId",
                table: "AuctionProductUsers");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "AuctionProductUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AuctionProductId",
                table: "AuctionProductUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "AuctionProducts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionProductUsers_AuctionProducts_AuctionProductId",
                table: "AuctionProductUsers",
                column: "AuctionProductId",
                principalTable: "AuctionProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionProductUsers_AuctionProducts_AuctionProductId",
                table: "AuctionProductUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AuctionProductId",
                table: "AuctionProductUsers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "AuctionProductUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "AuctionProducts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionProductUsers_ProductId",
                table: "AuctionProductUsers",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionProductUsers_AuctionProducts_AuctionProductId",
                table: "AuctionProductUsers",
                column: "AuctionProductId",
                principalTable: "AuctionProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionProductUsers_Products_ProductId",
                table: "AuctionProductUsers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
