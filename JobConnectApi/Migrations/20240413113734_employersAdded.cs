using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobConnectApi.Migrations
{
    /// <inheritdoc />
    public partial class employersAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AcceptedBy",
                table: "Jobs",
                newName: "EmployerId1");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Industry",
                table: "AspNetUsers",
                type: "TEXT",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_EmployerId1",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_EmployerId1",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Industry",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "EmployerId1",
                table: "Jobs",
                newName: "AcceptedBy");
        }
    }
}
