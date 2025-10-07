using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class foreignfix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_StoreModels_StoreModelStoreId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_StoreModelStoreId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "StoreModelStoreId",
                table: "Addresses");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "Addresses",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId1",
                table: "Addresses",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StoreId",
                table: "Addresses",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StoreId1",
                table: "Addresses",
                column: "StoreId1");

            migrationBuilder.CreateIndex(
                name: "IX_Additionals_ProductId",
                table: "Additionals",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Additionals_Products_ProductId",
                table: "Additionals",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_StoreModels_StoreId",
                table: "Addresses",
                column: "StoreId",
                principalTable: "StoreModels",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_StoreModels_StoreId1",
                table: "Addresses",
                column: "StoreId1",
                principalTable: "StoreModels",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Additionals_Products_ProductId",
                table: "Additionals");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_StoreModels_StoreId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_StoreModels_StoreId1",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_StoreId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_StoreId1",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Additionals_ProductId",
                table: "Additionals");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "StoreId1",
                table: "Addresses");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreModelStoreId",
                table: "Addresses",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StoreModelStoreId",
                table: "Addresses",
                column: "StoreModelStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_StoreModels_StoreModelStoreId",
                table: "Addresses",
                column: "StoreModelStoreId",
                principalTable: "StoreModels",
                principalColumn: "StoreId");
        }
    }
}
