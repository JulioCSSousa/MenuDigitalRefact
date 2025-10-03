using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class nine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "Products",
                newName: "StoreId");

            migrationBuilder.AddColumn<string>(
                name: "StoreUrl",
                table: "StoreModels",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreUrl",
                table: "StoreModels");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Products",
                newName: "MenuId");
        }
    }
}
