using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Categorise.Data.Migrations
{
    public partial class vendortransactionreverse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Transactions_TransactionForeignKey",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_TransactionForeignKey",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "TransactionForeignKey",
                table: "Vendors");

            migrationBuilder.AddColumn<Guid>(
                name: "VendorForeignKey",
                table: "Transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VendorId",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_VendorForeignKey",
                table: "Transactions",
                column: "VendorForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Vendors_VendorForeignKey",
                table: "Transactions",
                column: "VendorForeignKey",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Vendors_VendorForeignKey",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_VendorForeignKey",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "VendorForeignKey",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionForeignKey",
                table: "Vendors",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_TransactionForeignKey",
                table: "Vendors",
                column: "TransactionForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Transactions_TransactionForeignKey",
                table: "Vendors",
                column: "TransactionForeignKey",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
