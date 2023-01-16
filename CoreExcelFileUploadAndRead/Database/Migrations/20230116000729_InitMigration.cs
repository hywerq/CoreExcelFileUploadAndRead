using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreExcelFileUploadAndRead.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BalanceAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    OpeningBalanceActive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningBalancePassive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TurnoverDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TurnoverCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalanceActive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalancePassive = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningBalanceActive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningBalancePassive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TurnoverDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TurnoverCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalanceActive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalancePassive = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    OpeningBalanceActive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningBalancePassive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TurnoverDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TurnoverCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalanceActive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalancePassive = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrintTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    PeriodStart = table.Column<DateTime>(type: "date", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "date", nullable: false),
                    OpeningBalanceActive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningBalancePassive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TurnoverDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TurnoverCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalanceActive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalancePassive = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelFileId = table.Column<int>(type: "int", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: true),
                    ClassGroupId = table.Column<int>(type: "int", nullable: true),
                    BalanceAccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileDatas_BalanceAccounts_BalanceAccountId",
                        column: x => x.BalanceAccountId,
                        principalTable: "BalanceAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FileDatas_ClassGroups_ClassGroupId",
                        column: x => x.ClassGroupId,
                        principalTable: "ClassGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FileDatas_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FileDatas_Files_ExcelFileId",
                        column: x => x.ExcelFileId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileDatas_BalanceAccountId",
                table: "FileDatas",
                column: "BalanceAccountId",
                unique: true,
                filter: "[BalanceAccountId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FileDatas_ClassGroupId",
                table: "FileDatas",
                column: "ClassGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FileDatas_ClassId",
                table: "FileDatas",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_FileDatas_ExcelFileId",
                table: "FileDatas",
                column: "ExcelFileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileDatas");

            migrationBuilder.DropTable(
                name: "BalanceAccounts");

            migrationBuilder.DropTable(
                name: "ClassGroups");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
