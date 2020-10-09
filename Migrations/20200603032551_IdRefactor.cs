#pragma warning disable 1591
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Categorise.Migrations
{
    public partial class IdRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserForeignKey",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UserForeignKey",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionNotes_Transactions_TransactionForeignKey",
                table: "TransactionNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionNotes_Users_UserForeignKey",
                table: "TransactionNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionParties_Transactions_TransactionForeignKey",
                table: "TransactionParties");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionParties_Users_UserForeignKey",
                table: "TransactionParties");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountForeignKey",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryForeignKey",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserForeignKey",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionTags_Transactions_TransactionForeignKey",
                table: "TransactionTags");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionTags_Users_UserForeignKey",
                table: "TransactionTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionTags",
                table: "TransactionTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionParties",
                table: "TransactionParties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionNotes",
                table: "TransactionNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TransactionTagId",
                table: "TransactionTags");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionPartyId",
                table: "TransactionParties");

            migrationBuilder.DropColumn(
                name: "TransactionNoteId",
                table: "TransactionNotes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Accounts");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TransactionTags",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Transactions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TransactionParties",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TransactionNotes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Categories",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Accounts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionTags",
                table: "TransactionTags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionParties",
                table: "TransactionParties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionNotes",
                table: "TransactionNotes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserForeignKey",
                table: "Accounts",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserForeignKey",
                table: "Categories",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionNotes_Transactions_TransactionForeignKey",
                table: "TransactionNotes",
                column: "TransactionForeignKey",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionNotes_Users_UserForeignKey",
                table: "TransactionNotes",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionParties_Transactions_TransactionForeignKey",
                table: "TransactionParties",
                column: "TransactionForeignKey",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionParties_Users_UserForeignKey",
                table: "TransactionParties",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountForeignKey",
                table: "Transactions",
                column: "AccountForeignKey",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryForeignKey",
                table: "Transactions",
                column: "CategoryForeignKey",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserForeignKey",
                table: "Transactions",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionTags_Transactions_TransactionForeignKey",
                table: "TransactionTags",
                column: "TransactionForeignKey",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionTags_Users_UserForeignKey",
                table: "TransactionTags",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserForeignKey",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UserForeignKey",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionNotes_Transactions_TransactionForeignKey",
                table: "TransactionNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionNotes_Users_UserForeignKey",
                table: "TransactionNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionParties_Transactions_TransactionForeignKey",
                table: "TransactionParties");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionParties_Users_UserForeignKey",
                table: "TransactionParties");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountForeignKey",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryForeignKey",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserForeignKey",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionTags_Transactions_TransactionForeignKey",
                table: "TransactionTags");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionTags_Users_UserForeignKey",
                table: "TransactionTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionTags",
                table: "TransactionTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionParties",
                table: "TransactionParties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionNotes",
                table: "TransactionNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TransactionTags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TransactionParties");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TransactionNotes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Accounts");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionTagId",
                table: "TransactionTags",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionPartyId",
                table: "TransactionParties",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionNoteId",
                table: "TransactionNotes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionTags",
                table: "TransactionTags",
                column: "TransactionTagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "TransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionParties",
                table: "TransactionParties",
                column: "TransactionPartyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionNotes",
                table: "TransactionNotes",
                column: "TransactionNoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserForeignKey",
                table: "Accounts",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserForeignKey",
                table: "Categories",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionNotes_Transactions_TransactionForeignKey",
                table: "TransactionNotes",
                column: "TransactionForeignKey",
                principalTable: "Transactions",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionNotes_Users_UserForeignKey",
                table: "TransactionNotes",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionParties_Transactions_TransactionForeignKey",
                table: "TransactionParties",
                column: "TransactionForeignKey",
                principalTable: "Transactions",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionParties_Users_UserForeignKey",
                table: "TransactionParties",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountForeignKey",
                table: "Transactions",
                column: "AccountForeignKey",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryForeignKey",
                table: "Transactions",
                column: "CategoryForeignKey",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserForeignKey",
                table: "Transactions",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionTags_Transactions_TransactionForeignKey",
                table: "TransactionTags",
                column: "TransactionForeignKey",
                principalTable: "Transactions",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionTags_Users_UserForeignKey",
                table: "TransactionTags",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
#pragma warning restore 1591