using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syper.Migrations
{
    /// <inheritdoc />
    public partial class AdjustWorkoutTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppWorkouts_AppClients_AuthorId",
                table: "AppWorkouts");

            migrationBuilder.DropIndex(
                name: "IX_AppWorkouts_AuthorId",
                table: "AppWorkouts");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "AppWorkouts");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "AppWorkouts",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "AppWorkouts",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "AppWorkouts");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AppWorkouts",
                newName: "Title");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "AppWorkouts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkouts_AuthorId",
                table: "AppWorkouts",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppWorkouts_AppClients_AuthorId",
                table: "AppWorkouts",
                column: "AuthorId",
                principalTable: "AppClients",
                principalColumn: "Id");
        }
    }
}
