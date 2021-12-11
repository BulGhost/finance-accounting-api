using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceAccounting.DataAccess.Migrations
{
    public partial class UserEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IncomeCategory_UserId",
                table: "IncomeCategories");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseCategory_UserId",
                table: "ExpenseCategories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "IncomeCategories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExpenseCategories");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategoryUser",
                columns: table => new
                {
                    ExpenseCategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategoryUser", x => new { x.ExpenseCategoriesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ExpenseCategoryUser_ExpenseCategories_ExpenseCategoriesId",
                        column: x => x.ExpenseCategoriesId,
                        principalTable: "ExpenseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseCategoryUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncomeCategoryUser",
                columns: table => new
                {
                    IncomeCategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeCategoryUser", x => new { x.IncomeCategoriesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_IncomeCategoryUser_IncomeCategories_IncomeCategoriesId",
                        column: x => x.IncomeCategoriesId,
                        principalTable: "IncomeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IncomeCategoryUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategory_CategoryName",
                table: "ExpenseCategories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategoryUser_UsersId",
                table: "ExpenseCategoryUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeCategoryUser_UsersId",
                table: "IncomeCategoryUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseRecords_Users_UserId",
                table: "ExpenseRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeRecords_Users_UserId",
                table: "IncomeRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseRecords_Users_UserId",
                table: "ExpenseRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeRecords_Users_UserId",
                table: "IncomeRecords");

            migrationBuilder.DropTable(
                name: "ExpenseCategoryUser");

            migrationBuilder.DropTable(
                name: "IncomeCategoryUser");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseCategory_CategoryName",
                table: "ExpenseCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "IncomeCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ExpenseCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_IncomeCategory_UserId",
                table: "IncomeCategories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategory_UserId",
                table: "ExpenseCategories",
                column: "UserId");
        }
    }
}
