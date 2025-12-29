using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syper.Migrations
{
    /// <inheritdoc />
    public partial class RefactorPrograms5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppScheduleDays_AppPrograms_ProgramId1",
                table: "AppScheduleDays");

            migrationBuilder.DropIndex(
                name: "IX_AppScheduleDays_ProgramId1",
                table: "AppScheduleDays");

            migrationBuilder.DropColumn(
                name: "ProgramId1",
                table: "AppScheduleDays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProgramId1",
                table: "AppScheduleDays",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppScheduleDays_ProgramId1",
                table: "AppScheduleDays",
                column: "ProgramId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppScheduleDays_AppPrograms_ProgramId1",
                table: "AppScheduleDays",
                column: "ProgramId1",
                principalTable: "AppPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
