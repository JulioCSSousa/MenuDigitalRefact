using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class category1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_StoreModels_StoreId1",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_StoreId1",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "StoreId1",
                table: "Addresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreId1",
                table: "Addresses",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StoreId1",
                table: "Addresses",
                column: "StoreId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_StoreModels_StoreId1",
                table: "Addresses",
                column: "StoreId1",
                principalTable: "StoreModels",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
