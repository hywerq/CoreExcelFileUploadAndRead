using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreExcelFileUploadAndRead.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(256)", nullable: false),
                    Title = table.Column<string>(type: "varchar(256)", nullable: false),
                    Subject = table.Column<string>(type: "varchar(256)", nullable: false),
                    Currency = table.Column<string>(type: "varchar(256)", nullable: false),
                    PrintTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "date", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "date", nullable: false),
                    OpeningBalanceActive = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    OpeningBalancePassive = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    TurnoverDebit = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    TurnoverCredit = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    ClosingBalanceActive = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    ClosingBalancePassive = table.Column<decimal>(type: "decimal(20,2)", nullable: false)
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
                name: "Files");
        }
    }
}
