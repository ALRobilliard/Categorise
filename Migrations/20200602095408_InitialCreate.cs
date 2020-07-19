using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CategoriseApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 25, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    IsRegistered = table.Column<bool>(nullable: true),
                    ConfirmedEmail = table.Column<bool>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(nullable: false),
                    AccountName = table.Column<string>(maxLength: 25, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    UserForeignKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserForeignKey",
                        column: x => x.UserForeignKey,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(nullable: false),
                    CategoryName = table.Column<string>(maxLength: 25, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    UserForeignKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Users_UserForeignKey",
                        column: x => x.UserForeignKey,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(nullable: false),
                    TransactionType = table.Column<string>(maxLength: 25, nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    IsShared = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    AccountForeignKey = table.Column<Guid>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: false),
                    CategoryForeignKey = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    UserForeignKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountForeignKey",
                        column: x => x.AccountForeignKey,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryForeignKey",
                        column: x => x.CategoryForeignKey,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserForeignKey",
                        column: x => x.UserForeignKey,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionNotes",
                columns: table => new
                {
                    TransactionNoteId = table.Column<Guid>(nullable: false),
                    TransactionNoteSubject = table.Column<string>(maxLength: 50, nullable: false),
                    TransactionNoteBody = table.Column<string>(maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: false),
                    TransactionForeignKey = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    UserForeignKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionNotes", x => x.TransactionNoteId);
                    table.ForeignKey(
                        name: "FK_TransactionNotes_Transactions_TransactionForeignKey",
                        column: x => x.TransactionForeignKey,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionNotes_Users_UserForeignKey",
                        column: x => x.UserForeignKey,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionParties",
                columns: table => new
                {
                    TransactionPartyId = table.Column<Guid>(nullable: false),
                    TransactionPartyName = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: false),
                    TransactionForeignKey = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    UserForeignKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionParties", x => x.TransactionPartyId);
                    table.ForeignKey(
                        name: "FK_TransactionParties_Transactions_TransactionForeignKey",
                        column: x => x.TransactionForeignKey,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionParties_Users_UserForeignKey",
                        column: x => x.UserForeignKey,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTags",
                columns: table => new
                {
                    TransactionTagId = table.Column<Guid>(nullable: false),
                    TransactionTagName = table.Column<string>(maxLength: 25, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: false),
                    TransactionForeignKey = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    UserForeignKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTags", x => x.TransactionTagId);
                    table.ForeignKey(
                        name: "FK_TransactionTags_Transactions_TransactionForeignKey",
                        column: x => x.TransactionForeignKey,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionTags_Users_UserForeignKey",
                        column: x => x.UserForeignKey,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserForeignKey",
                table: "Accounts",
                column: "UserForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserForeignKey",
                table: "Categories",
                column: "UserForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionNotes_TransactionForeignKey",
                table: "TransactionNotes",
                column: "TransactionForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionNotes_UserForeignKey",
                table: "TransactionNotes",
                column: "UserForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionParties_TransactionForeignKey",
                table: "TransactionParties",
                column: "TransactionForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionParties_UserForeignKey",
                table: "TransactionParties",
                column: "UserForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountForeignKey",
                table: "Transactions",
                column: "AccountForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryForeignKey",
                table: "Transactions",
                column: "CategoryForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserForeignKey",
                table: "Transactions",
                column: "UserForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTags_TransactionForeignKey",
                table: "TransactionTags",
                column: "TransactionForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTags_UserForeignKey",
                table: "TransactionTags",
                column: "UserForeignKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionNotes");

            migrationBuilder.DropTable(
                name: "TransactionParties");

            migrationBuilder.DropTable(
                name: "TransactionTags");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
