using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syper.Migrations
{
    /// <inheritdoc />
    public partial class RefactorPrograms6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppScheduleActivities_AppScheduleDays_ScheduleDayId1",
                table: "AppScheduleActivities");

            migrationBuilder.DropIndex(
                name: "IX_AppScheduleActivities_ScheduleDayId1",
                table: "AppScheduleActivities");

            migrationBuilder.DropColumn(
                name: "ScheduleDayId1",
                table: "AppScheduleActivities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleDayId1",
                table: "AppScheduleActivities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppScheduleActivities_ScheduleDayId1",
                table: "AppScheduleActivities",
                column: "ScheduleDayId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppScheduleActivities_AppScheduleDays_ScheduleDayId1",
                table: "AppScheduleActivities",
                column: "ScheduleDayId1",
                principalTable: "AppScheduleDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
