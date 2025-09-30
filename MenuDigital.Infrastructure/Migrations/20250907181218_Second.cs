using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "StoreModels");

            migrationBuilder.DropColumn(
                name: "PaymentForm",
                table: "StoreModels");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Street = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    District = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Complement = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ZipCode = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StoreModelStoreId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_StoreModels_StoreModelStoreId",
                        column: x => x.StoreModelStoreId,
                        principalTable: "StoreModels",
                        principalColumn: "StoreId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StorePayments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PaymentsCount = table.Column<int>(type: "int", nullable: true),
                    StoreModelStoreId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorePayments_StoreModels_StoreModelStoreId",
                        column: x => x.StoreModelStoreId,
                        principalTable: "StoreModels",
                        principalColumn: "StoreId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StoreModelStoreId",
                table: "Addresses",
                column: "StoreModelStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StorePayments_StoreModelStoreId",
                table: "StorePayments",
                column: "StoreModelStoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "StorePayments");

            migrationBuilder.AddColumn<long>(
                name: "AddressId",
                table: "StoreModels",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentForm",
                table: "StoreModels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
