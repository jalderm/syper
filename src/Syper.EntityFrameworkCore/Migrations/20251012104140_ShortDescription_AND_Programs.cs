using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syper.Migrations
{
    /// <inheritdoc />
    public partial class ShortDescription_AND_Programs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "AppWorkoutSections",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Repeats",
                table: "AppWorkoutExercises",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PerceivedEffort",
                table: "AppSets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentageOfMax",
                table: "AppSets",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutExerciseId1",
                table: "AppSets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "AppClientCoachSubscriptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppClientCoachSubscriptions_AppClients_ClientId",
                table: "AppClientCoachSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSets_AppWorkoutExercises_WorkoutExerciseId1",
                table: "AppSets");

            migrationBuilder.DropIndex(
                name: "IX_AppSets_WorkoutExerciseId1",
                table: "AppSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppClientCoachSubscriptions",
                table: "AppClientCoachSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_AppClientCoachSubscriptions_ClientId_TenantId",
                table: "AppClientCoachSubscriptions");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "AppWorkoutSections");

            migrationBuilder.DropColumn(
                name: "Repeats",
                table: "AppWorkoutExercises");

            migrationBuilder.DropColumn(
                name: "PerceivedEffort",
                table: "AppSets");

            migrationBuilder.DropColumn(
                name: "PercentageOfMax",
                table: "AppSets");

            migrationBuilder.DropColumn(
                name: "WorkoutExerciseId1",
                table: "AppSets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppClientCoachSubscriptions");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AppClientCoachSubscriptions");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppClientCoachSubscriptions");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppClientCoachSubscriptions");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppClientCoachSubscriptions");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppClientCoachSubscriptions");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "AppClientCoachSubscriptions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppClientCoachSubscriptions");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppClientCoachSubscriptions");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppClientCoachSubscriptions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppClientCoachSubscriptions");

            migrationBuilder.RenameTable(
                name: "AppClientCoachSubscriptions",
                newName: "ClientCoachSubscription");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "ClientCoachSubscription",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCoachSubscription_AppClients_ClientId",
                table: "ClientCoachSubscription",
                column: "ClientId",
                principalTable: "AppClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
