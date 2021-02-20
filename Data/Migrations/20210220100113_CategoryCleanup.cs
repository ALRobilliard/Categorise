using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Categorise.Data.Migrations
{
    public partial class CategoryCleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryForeignKey",
                table: "Vendors",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Vendors",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGlobal",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_CategoryForeignKey",
                table: "Vendors",
                column: "CategoryForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Categories_CategoryForeignKey",
                table: "Vendors",
                column: "CategoryForeignKey",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Categories_CategoryForeignKey",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_CategoryForeignKey",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CategoryForeignKey",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "IsGlobal",
                table: "Categories");
        }
    }
}
