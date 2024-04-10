using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobConnectApi.Migrations
{
    /// <inheritdoc />
    public partial class JobsTableModified1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_EmployerId1",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_EmployerId1",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "EmployerId1",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "AcceptedBy",
                table: "Jobs",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "EmployerId",
                table: "Jobs",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Jobs",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "JobDescription",
                table: "Jobs",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Jobs",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "JobType",
                table: "Jobs",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "Jobs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Salray",
                table: "Jobs",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Jobs",
                type: "longtext",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_EmployerId",
                table: "Jobs",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_EmployerId",
                table: "Jobs",
                column: "EmployerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_EmployerId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_EmployerId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "AcceptedBy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobDescription",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Salray",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "EmployerId1",
                table: "Jobs",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_EmployerId1",
                table: "Jobs",
                column: "EmployerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_EmployerId1",
                table: "Jobs",
                column: "EmployerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
