using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KASHOP.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addBrandId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Product_ProductId",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_ProductId",
                table: "Brand");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreateById",
                table: "Brand",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Brand",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "Brand",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdateById",
                table: "Brand",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Brand",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_CreateById",
                table: "Brand",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_ProductId1",
                table: "Brand",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_UpdateById",
                table: "Brand",
                column: "UpdateById");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "IX_Brand_CreateById",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_ProductId1",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_UpdateById",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "UpdateById",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Brand");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_ProductId",
                table: "Brand",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Product_ProductId",
                table: "Brand",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
