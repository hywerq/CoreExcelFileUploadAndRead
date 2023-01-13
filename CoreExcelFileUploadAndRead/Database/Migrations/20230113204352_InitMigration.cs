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
                name: "FileDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelFileId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    ClassGroupId = table.Column<int>(type: "int", nullable: false),
                    BalanceAccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDatas", x => x.Id);
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceAccounts");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "ClassGroups");

            migrationBuilder.DropTable(
                name: "FileDatas");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
