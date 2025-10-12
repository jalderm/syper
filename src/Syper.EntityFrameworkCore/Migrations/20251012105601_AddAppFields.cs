using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syper.Migrations
{
    /// <inheritdoc />
    public partial class AddAppFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentageOfMax",
                table: "AppSets");

            migrationBuilder.AlterColumn<int>(
                name: "Repeats",
                table: "AppWorkoutExercises",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PerceivedEffort",
                table: "AppSets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<decimal>(
                name: "UpperPercentageOfMax",
                table: "AppSets",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpperPercentageOfMax",
                table: "AppSets");

            migrationBuilder.AlterColumn<int>(
                name: "Repeats",
                table: "AppWorkoutExercises",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PerceivedEffort",
                table: "AppSets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentageOfMax",
                table: "AppSets",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
