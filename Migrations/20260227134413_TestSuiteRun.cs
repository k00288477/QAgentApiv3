using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QAgentApi.Migrations
{
    /// <inheritdoc />
    public partial class TestSuiteRun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SuiteRunId",
                table: "ExecutionReports",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TestSuiteRuns",
                columns: table => new
                {
                    SuiteRunId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TestSuiteId = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalTests = table.Column<int>(type: "int", nullable: false),
                    CompletedTests = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSuiteRuns", x => x.SuiteRunId);
                    table.ForeignKey(
                        name: "FK_TestSuiteRuns_TestSuites_TestSuiteId",
                        column: x => x.TestSuiteId,
                        principalTable: "TestSuites",
                        principalColumn: "TestSuiteId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionReports_SuiteRunId",
                table: "ExecutionReports",
                column: "SuiteRunId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSuiteRuns_TestSuiteId",
                table: "TestSuiteRuns",
                column: "TestSuiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExecutionReports_TestSuiteRuns_SuiteRunId",
                table: "ExecutionReports",
                column: "SuiteRunId",
                principalTable: "TestSuiteRuns",
                principalColumn: "SuiteRunId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExecutionReports_TestSuiteRuns_SuiteRunId",
                table: "ExecutionReports");

            migrationBuilder.DropTable(
                name: "TestSuiteRuns");

            migrationBuilder.DropIndex(
                name: "IX_ExecutionReports_SuiteRunId",
                table: "ExecutionReports");

            migrationBuilder.DropColumn(
                name: "SuiteRunId",
                table: "ExecutionReports");
        }
    }
}
