using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syper.Migrations
{
    /// <inheritdoc />
    public partial class RefactorScheduleWeeksToDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppScheduleDays_AppWeeklySchedules_WeeklyScheduleId",
                table: "AppScheduleDays");

            migrationBuilder.DropForeignKey(
                name: "FK_AppScheduleDays_AppWeeklySchedules_WeeklyScheduleId1",
                table: "AppScheduleDays");

            migrationBuilder.DropTable(
                name: "AppWeeklySchedules");

            migrationBuilder.RenameColumn(
                name: "WeeklyScheduleId1",
                table: "AppScheduleDays",
                newName: "ProgramId1");

            migrationBuilder.RenameColumn(
                name: "WeeklyScheduleId",
                table: "AppScheduleDays",
                newName: "ProgramId");

            migrationBuilder.RenameColumn(
                name: "DayOfWeek",
                table: "AppScheduleDays",
                newName: "DayOffSet");

            migrationBuilder.RenameIndex(
                name: "IX_AppScheduleDays_WeeklyScheduleId1",
                table: "AppScheduleDays",
                newName: "IX_AppScheduleDays_ProgramId1");

            migrationBuilder.RenameIndex(
                name: "IX_AppScheduleDays_WeeklyScheduleId",
                table: "AppScheduleDays",
                newName: "IX_AppScheduleDays_ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppScheduleDays_AppPrograms_ProgramId",
                table: "AppScheduleDays",
                column: "ProgramId",
                principalTable: "AppPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppScheduleDays_AppPrograms_ProgramId1",
                table: "AppScheduleDays",
                column: "ProgramId1",
                principalTable: "AppPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppScheduleDays_AppPrograms_ProgramId",
                table: "AppScheduleDays");

            migrationBuilder.DropForeignKey(
                name: "FK_AppScheduleDays_AppPrograms_ProgramId1",
                table: "AppScheduleDays");

            migrationBuilder.RenameColumn(
                name: "ProgramId1",
                table: "AppScheduleDays",
                newName: "WeeklyScheduleId1");

            migrationBuilder.RenameColumn(
                name: "ProgramId",
                table: "AppScheduleDays",
                newName: "WeeklyScheduleId");

            migrationBuilder.RenameColumn(
                name: "DayOffSet",
                table: "AppScheduleDays",
                newName: "DayOfWeek");

            migrationBuilder.RenameIndex(
                name: "IX_AppScheduleDays_ProgramId1",
                table: "AppScheduleDays",
                newName: "IX_AppScheduleDays_WeeklyScheduleId1");

            migrationBuilder.RenameIndex(
                name: "IX_AppScheduleDays_ProgramId",
                table: "AppScheduleDays",
                newName: "IX_AppScheduleDays_WeeklyScheduleId");

            migrationBuilder.CreateTable(
                name: "AppWeeklySchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgramId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProgramId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    notes = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWeeklySchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppWeeklySchedules_AppPrograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "AppPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppWeeklySchedules_AppPrograms_ProgramId1",
                        column: x => x.ProgramId1,
                        principalTable: "AppPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppWeeklySchedules_ProgramId",
                table: "AppWeeklySchedules",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWeeklySchedules_ProgramId1",
                table: "AppWeeklySchedules",
                column: "ProgramId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppScheduleDays_AppWeeklySchedules_WeeklyScheduleId",
                table: "AppScheduleDays",
                column: "WeeklyScheduleId",
                principalTable: "AppWeeklySchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppScheduleDays_AppWeeklySchedules_WeeklyScheduleId1",
                table: "AppScheduleDays",
                column: "WeeklyScheduleId1",
                principalTable: "AppWeeklySchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
