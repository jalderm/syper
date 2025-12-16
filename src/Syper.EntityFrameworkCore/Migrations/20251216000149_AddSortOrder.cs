using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syper.Migrations
{
    /// <inheritdoc />
    public partial class AddSortOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpperPercentageOfMax",
                table: "AppSets",
                newName: "UpperPercentageOfMaxHR");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "AppWorkoutSections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "AppWorkoutExercises",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "AppSets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "AppWorkoutSections");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "AppWorkoutExercises");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "AppSets");

            migrationBuilder.RenameColumn(
                name: "UpperPercentageOfMaxHR",
                table: "AppSets",
                newName: "UpperPercentageOfMax");
        }
    }
}
