using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seven : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Menu_MenuModelMenuId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MenuModelMenuId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MenuModelMenuId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "MenuName",
                keyValue: null,
                column: "MenuName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "MenuName",
                table: "Menu",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ProductIds",
                table: "Menu",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductIds",
                table: "Menu");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuModelMenuId",
                table: "Products",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "MenuName",
                table: "Menu",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MenuModelMenuId",
                table: "Products",
                column: "MenuModelMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Menu_MenuModelMenuId",
                table: "Products",
                column: "MenuModelMenuId",
                principalTable: "Menu",
                principalColumn: "MenuId");
        }
    }
}
