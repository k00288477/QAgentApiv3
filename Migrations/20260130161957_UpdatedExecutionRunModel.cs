using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QAgentApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedExecutionRunModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "ExecutionRuns");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "ExecutionRuns",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "ExecutionRuns",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time(6)");

            migrationBuilder.AddColumn<string>(
                name: "CheckStatusUrl",
                table: "ExecutionRuns",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "ExecutionRuns",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "RecordingEnabled",
                table: "ExecutionRuns",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Success",
                table: "ExecutionRuns",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Task_id",
                table: "ExecutionRuns",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckStatusUrl",
                table: "ExecutionRuns");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "ExecutionRuns");

            migrationBuilder.DropColumn(
                name: "RecordingEnabled",
                table: "ExecutionRuns");

            migrationBuilder.DropColumn(
                name: "Success",
                table: "ExecutionRuns");

            migrationBuilder.DropColumn(
                name: "Task_id",
                table: "ExecutionRuns");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ExecutionRuns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "StartTime",
                table: "ExecutionRuns",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndTime",
                table: "ExecutionRuns",
                type: "time(6)",
                nullable: true);
        }
    }
}
