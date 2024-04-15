using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobConnectApi.Migrations
{
    /// <inheritdoc />
    public partial class ProposalFkTesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_EmployerId1",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Jobs_JobId1",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_JobId1",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_EmployerId1",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobId1",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "EmployerId1",
                table: "Jobs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobId1",
                table: "Proposals",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployerId1",
                table: "Jobs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_JobId1",
                table: "Proposals",
                column: "JobId1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Jobs_JobId1",
                table: "Proposals",
                column: "JobId1",
                principalTable: "Jobs",
                principalColumn: "JobId");
        }
    }
}
