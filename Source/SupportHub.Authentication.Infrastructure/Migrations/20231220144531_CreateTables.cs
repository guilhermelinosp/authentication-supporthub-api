using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupportHub.Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "TB_Authentication_Company");

            migrationBuilder.DropColumn(
                name: "DisabledAt",
                table: "TB_Authentication_Company");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "TB_Authentication_Company");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "TB_Authentication_Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DisabledAt",
                table: "TB_Authentication_Company",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "TB_Authentication_Company",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
