using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreSocialMedias_StoreModels_StoreModelStoreId",
                table: "StoreSocialMedias");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedules_StoreModels_StoreModelStoreId",
                table: "WorkSchedules");

            migrationBuilder.RenameColumn(
                name: "StoreModelStoreId",
                table: "WorkSchedules",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSchedules_StoreModelStoreId",
                table: "WorkSchedules",
                newName: "IX_WorkSchedules_StoreId");

            migrationBuilder.RenameColumn(
                name: "StoreModelStoreId",
                table: "StoreSocialMedias",
                newName: "StoreId");

            migrationBuilder.AddColumn<string>(
                name: "ActivedPlan",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreSocialMedias_StoreModels_StoreId",
                table: "StoreSocialMedias",
                column: "StoreId",
                principalTable: "StoreModels",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedules_StoreModels_StoreId",
                table: "WorkSchedules",
                column: "StoreId",
                principalTable: "StoreModels",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreSocialMedias_StoreModels_StoreId",
                table: "StoreSocialMedias");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedules_StoreModels_StoreId",
                table: "WorkSchedules");

            migrationBuilder.DropColumn(
                name: "ActivedPlan",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "WorkSchedules",
                newName: "StoreModelStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSchedules_StoreId",
                table: "WorkSchedules",
                newName: "IX_WorkSchedules_StoreModelStoreId");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "StoreSocialMedias",
                newName: "StoreModelStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreSocialMedias_StoreModels_StoreModelStoreId",
                table: "StoreSocialMedias",
                column: "StoreModelStoreId",
                principalTable: "StoreModels",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedules_StoreModels_StoreModelStoreId",
                table: "WorkSchedules",
                column: "StoreModelStoreId",
                principalTable: "StoreModels",
                principalColumn: "StoreId");
        }
    }
}
