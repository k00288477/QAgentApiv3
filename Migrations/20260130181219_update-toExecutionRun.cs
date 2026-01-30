using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QAgentApi.Migrations
{
    /// <inheritdoc />
    public partial class updatetoExecutionRun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReportUrl",
                table: "ExecutionRuns",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportUrl",
                table: "ExecutionRuns");
        }
    }
}
