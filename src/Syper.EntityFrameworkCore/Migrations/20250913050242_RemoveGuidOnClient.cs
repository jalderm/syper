using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syper.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGuidOnClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "AppClients");

            migrationBuilder.AddColumn<int>(
                name: "ClientState",
                table: "AppClients",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientState",
                table: "AppClients");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "AppClients",
                type: "uuid",
                maxLength: 32,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
