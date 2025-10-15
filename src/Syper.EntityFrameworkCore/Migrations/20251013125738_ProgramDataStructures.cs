using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syper.Migrations
{
    /// <inheritdoc />
    public partial class ProgramDataStructures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppPrograms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Goal = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ShortDescription = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppWeeklySchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    notes = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ProgramId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgramId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "AppScheduleDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    WeeklyScheduleId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeeklyScheduleId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppScheduleDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppScheduleDays_AppWeeklySchedules_WeeklyScheduleId",
                        column: x => x.WeeklyScheduleId,
                        principalTable: "AppWeeklySchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppScheduleDays_AppWeeklySchedules_WeeklyScheduleId1",
                        column: x => x.WeeklyScheduleId1,
                        principalTable: "AppWeeklySchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppScheduleActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uuid", nullable: true),
                    ScheduleDayId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScheduleDayId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppScheduleActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppScheduleActivities_AppScheduleDays_ScheduleDayId",
                        column: x => x.ScheduleDayId,
                        principalTable: "AppScheduleDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppScheduleActivities_AppScheduleDays_ScheduleDayId1",
                        column: x => x.ScheduleDayId1,
                        principalTable: "AppScheduleDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppScheduleActivities_AppWorkouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "AppWorkouts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppScheduleActivities_ScheduleDayId",
                table: "AppScheduleActivities",
                column: "ScheduleDayId");

            migrationBuilder.CreateIndex(
                name: "IX_AppScheduleActivities_ScheduleDayId1",
                table: "AppScheduleActivities",
                column: "ScheduleDayId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppScheduleActivities_WorkoutId",
                table: "AppScheduleActivities",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_AppScheduleDays_WeeklyScheduleId",
                table: "AppScheduleDays",
                column: "WeeklyScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppScheduleDays_WeeklyScheduleId1",
                table: "AppScheduleDays",
                column: "WeeklyScheduleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppWeeklySchedules_ProgramId",
                table: "AppWeeklySchedules",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWeeklySchedules_ProgramId1",
                table: "AppWeeklySchedules",
                column: "ProgramId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppScheduleActivities");

            migrationBuilder.DropTable(
                name: "AppScheduleDays");

            migrationBuilder.DropTable(
                name: "AppWeeklySchedules");

            migrationBuilder.DropTable(
                name: "AppPrograms");
        }
    }
}
