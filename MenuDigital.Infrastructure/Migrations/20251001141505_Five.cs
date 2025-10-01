using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Five : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProductModel");

            migrationBuilder.DropTable(
                name: "CategoryStoreModel");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductModelProductId",
                table: "Categories",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreModelStoreId",
                table: "Categories",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductModelProductId",
                table: "Categories",
                column: "ProductModelProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_StoreModelStoreId",
                table: "Categories",
                column: "StoreModelStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Products_ProductModelProductId",
                table: "Categories",
                column: "ProductModelProductId",
                principalTable: "Products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_StoreModels_StoreModelStoreId",
                table: "Categories",
                column: "StoreModelStoreId",
                principalTable: "StoreModels",
                principalColumn: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Products_ProductModelProductId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_StoreModels_StoreModelStoreId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductModelProductId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_StoreModelStoreId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ProductModelProductId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "StoreModelStoreId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryProductModel",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ProductsProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProductModel", x => new { x.CategoryId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_CategoryProductModel_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProductModel_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CategoryStoreModel",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    StoresStoreId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryStoreModel", x => new { x.CategoryId, x.StoresStoreId });
                    table.ForeignKey(
                        name: "FK_CategoryStoreModel_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryStoreModel_StoreModels_StoresStoreId",
                        column: x => x.StoresStoreId,
                        principalTable: "StoreModels",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProductModel_ProductsProductId",
                table: "CategoryProductModel",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryStoreModel_StoresStoreId",
                table: "CategoryStoreModel",
                column: "StoresStoreId");
        }
    }
}
