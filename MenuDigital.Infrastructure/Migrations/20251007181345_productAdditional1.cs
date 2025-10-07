using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class productAdditional1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Additionals_Products_ProductId1",
                table: "Additionals");

            migrationBuilder.DropIndex(
                name: "IX_Additionals_ProductId1",
                table: "Additionals");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Additionals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "Additionals",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Additionals_ProductId1",
                table: "Additionals",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Additionals_Products_ProductId1",
                table: "Additionals",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
