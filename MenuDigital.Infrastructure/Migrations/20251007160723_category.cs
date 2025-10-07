using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_StoreModels_StoreModelStoreId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_StoreModelStoreId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "StoreModelStoreId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryStoreModel",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    StoreId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryStoreModel", x => new { x.CategoryId, x.StoreId });
                    table.ForeignKey(
                        name: "FK_CategoryStoreModel_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryStoreModel_StoreModels_StoreId",
                        column: x => x.StoreId,
                        principalTable: "StoreModels",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryStoreModel_StoreId",
                table: "CategoryStoreModel",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryStoreModel");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreModelStoreId",
                table: "Categories",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_StoreModelStoreId",
                table: "Categories",
                column: "StoreModelStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_StoreModels_StoreModelStoreId",
                table: "Categories",
                column: "StoreModelStoreId",
                principalTable: "StoreModels",
                principalColumn: "StoreId");
        }
    }
}
