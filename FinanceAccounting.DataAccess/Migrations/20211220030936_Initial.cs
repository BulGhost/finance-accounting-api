using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceAccounting.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.Sql("CREATE FUNCTION dbo.DefineTransactionType (@CategoryId INT) " +
                                 "RETURNS INT AS " +
                                 "BEGIN " +
                                    "DECLARE @TransactionType INT; " +
                                    "SELECT @TransactionType = Type " +
                                    "FROM Categories " +
                                    "WHERE Id = @CategoryId " +
                                    "RETURN @TransactionType " +
                                 "END");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false, computedColumnSql: "dbo.DefineTransactionType([CategoryId])"),
                    Sum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersCategories",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersCategories", x => new { x.CategoriesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UsersCategories_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersCategories_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "Type" },
                values: new object[,]
                {
                    { 1, "Salary", 0 },
                    { 27, "Services", 1 },
                    { 26, "Transport", 1 },
                    { 25, "Technique", 1 },
                    { 24, "Hygiene products", 1 },
                    { 23, "Repair", 1 },
                    { 22, "Regular payments", 1 },
                    { 21, "Entertainment", 1 },
                    { 20, "Gifts", 1 },
                    { 19, "Nutrition", 1 },
                    { 18, "Clothing and Footwear", 1 },
                    { 17, "Medicine", 1 },
                    { 16, "Furniture", 1 },
                    { 28, "Household goods", 1 },
                    { 15, "Utilities", 1 },
                    { 13, "Car", 1 },
                    { 12, "Insurance", 0 },
                    { 11, "Scholarship", 0 },
                    { 10, "Pension", 0 },
                    { 9, "Material aid", 0 },
                    { 8, "Subsidy", 0 },
                    { 7, "Rent", 0 },
                    { 6, "Inheritance", 0 },
                    { 5, "Part-time", 0 },
                    { 4, "Sale of property", 0 },
                    { 3, "Gift", 0 },
                    { 2, "Passive income", 0 },
                    { 14, "Charity", 1 },
                    { 29, "Commission", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersCategories_UsersId",
                table: "UsersCategories",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UsersCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.Sql("DROP FUNCTION dbo.DefineTransactionType");
        }
    }
}
