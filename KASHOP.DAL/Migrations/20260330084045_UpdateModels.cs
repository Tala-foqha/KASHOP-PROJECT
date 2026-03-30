using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KASHOP.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Product_ProductId1",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Users_CreateById",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Users_UpdateById",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_ProductId1",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Brand");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                table: "Product",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Users_CreateById",
                table: "Brand",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Users_UpdateById",
                table: "Brand",
                column: "UpdateById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Brand_BrandId",
                table: "Product",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Users_CreateById",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Users_UpdateById",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Brand_BrandId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_BrandId",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Brand",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "Brand",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_ProductId1",
                table: "Brand",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Product_ProductId1",
                table: "Brand",
                column: "ProductId1",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Users_CreateById",
                table: "Brand",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Users_UpdateById",
                table: "Brand",
                column: "UpdateById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
