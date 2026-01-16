using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QAgentApi.Migrations
{
    /// <inheritdoc />
    public partial class updatedTestStepModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "TestSteps",
                newName: "ExpectedResult");

            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "TestSteps",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "TestSteps");

            migrationBuilder.RenameColumn(
                name: "ExpectedResult",
                table: "TestSteps",
                newName: "Content");
        }
    }
}
