using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syper.Migrations
{
    /// <inheritdoc />
    public partial class AddExercises_Runs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppWorkoutExercises_Exercise_ExerciseId",
                table: "AppWorkoutExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise");

            migrationBuilder.RenameTable(
                name: "Exercise",
                newName: "AppExercises");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppExercises",
                table: "AppExercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppWorkoutExercises_AppExercises_ExerciseId",
                table: "AppWorkoutExercises",
                column: "ExerciseId",
                principalTable: "AppExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppWorkoutExercises_AppExercises_ExerciseId",
                table: "AppWorkoutExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppExercises",
                table: "AppExercises");

            migrationBuilder.RenameTable(
                name: "AppExercises",
                newName: "Exercise");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppWorkoutExercises_Exercise_ExerciseId",
                table: "AppWorkoutExercises",
                column: "ExerciseId",
                principalTable: "Exercise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
