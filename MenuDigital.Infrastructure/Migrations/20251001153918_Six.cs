using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Six : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Products",
                newName: "MenuId");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuModelMenuId",
                table: "Products",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    MenuId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MenuName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Menu_MenuModelMenuId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Products_MenuModelMenuId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MenuModelMenuId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "Products",
                newName: "StoreId");
        }
    }
}
