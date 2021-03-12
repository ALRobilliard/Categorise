using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Categorise.Data.Migrations
{
    public partial class VendorForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_AspNetUsers_UserForeignKey",
                table: "Vendors");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Categories_CategoryForeignKey",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_CategoryForeignKey",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_UserForeignKey",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CategoryForeignKey",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "UserForeignKey",
                table: "Vendors");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_CategoryId",
                table: "Vendors",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_UserId",
                table: "Vendors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_AspNetUsers_UserId",
                table: "Vendors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Categories_CategoryId",
                table: "Vendors",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_AspNetUsers_UserId",
                table: "Vendors");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Categories_CategoryId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_CategoryId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_UserId",
                table: "Vendors");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryForeignKey",
                table: "Vendors",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserForeignKey",
                table: "Vendors",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_CategoryForeignKey",
                table: "Vendors",
                column: "CategoryForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_UserForeignKey",
                table: "Vendors",
                column: "UserForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_AspNetUsers_UserForeignKey",
                table: "Vendors",
                column: "UserForeignKey",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Categories_CategoryForeignKey",
                table: "Vendors",
                column: "CategoryForeignKey",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
