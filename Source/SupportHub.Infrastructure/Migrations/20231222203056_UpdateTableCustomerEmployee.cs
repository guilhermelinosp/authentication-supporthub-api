using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupportHub.Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableCustomerEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_Authentication_Client",
                table: "TB_Authentication_Client");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "TB_Authentication_Employee");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "TB_Authentication_Employee");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "TB_Authentication_Client");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "TB_Authentication_Client");

            migrationBuilder.RenameTable(
                name: "TB_Authentication_Client",
                newName: "TB_Authentication_Customer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DisabledAt",
                table: "TB_Authentication_Employee",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DisabledAt",
                table: "TB_Authentication_Customer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_Authentication_Customer",
                table: "TB_Authentication_Customer",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_Authentication_Customer",
                table: "TB_Authentication_Customer");

            migrationBuilder.RenameTable(
                name: "TB_Authentication_Customer",
                newName: "TB_Authentication_Client");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DisabledAt",
                table: "TB_Authentication_Employee",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "TB_Authentication_Employee",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "TB_Authentication_Employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DisabledAt",
                table: "TB_Authentication_Client",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "TB_Authentication_Client",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "TB_Authentication_Client",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_Authentication_Client",
                table: "TB_Authentication_Client",
                column: "CustomerId");
        }
    }
}
