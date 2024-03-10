using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MMABackend.Migrations
{
    public partial class _18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "ProductPhotos",
                newName: "FileName");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "ProductPhotos",
                type: "BLOB",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "ProductPhotos");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "ProductPhotos",
                newName: "Path");
        }
    }
}
