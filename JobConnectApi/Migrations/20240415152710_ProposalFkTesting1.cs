using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobConnectApi.Migrations
{
    /// <inheritdoc />
    public partial class ProposalFkTesting1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobJobSeeker_Jobs_JobId",
                table: "JobJobSeeker");

            migrationBuilder.DropForeignKey(
                name: "FK_JobJobSeeker1_Jobs_Job1JobId",
                table: "JobJobSeeker1");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_AspNetUsers_JobSeekerId1",
                table: "Proposals");

            migrationBuilder.DropTable(
                name: "JobJobSeeker2");

            migrationBuilder.DropTable(
                name: "JobJobSeeker3");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_JobSeekerId1",
                table: "Proposals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobJobSeeker1",
                table: "JobJobSeeker1");

            migrationBuilder.DropIndex(
                name: "IX_JobJobSeeker1_SavedById",
                table: "JobJobSeeker1");

            migrationBuilder.DropColumn(
                name: "JobSeekerId1",
                table: "Proposals");

            migrationBuilder.RenameColumn(
                name: "Job1JobId",
                table: "JobJobSeeker1",
                newName: "SavedJobsJobId");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "JobJobSeeker",
                newName: "AppliedJobsJobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobJobSeeker_JobId",
                table: "JobJobSeeker",
                newName: "IX_JobJobSeeker_AppliedJobsJobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobJobSeeker1",
                table: "JobJobSeeker1",
                columns: new[] { "SavedById", "SavedJobsJobId" });

            migrationBuilder.CreateIndex(
                name: "IX_JobJobSeeker1_SavedJobsJobId",
                table: "JobJobSeeker1",
                column: "SavedJobsJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobJobSeeker_Jobs_AppliedJobsJobId",
                table: "JobJobSeeker",
                column: "AppliedJobsJobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobJobSeeker1_Jobs_SavedJobsJobId",
                table: "JobJobSeeker1",
                column: "SavedJobsJobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobJobSeeker_Jobs_AppliedJobsJobId",
                table: "JobJobSeeker");

            migrationBuilder.DropForeignKey(
                name: "FK_JobJobSeeker1_Jobs_SavedJobsJobId",
                table: "JobJobSeeker1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobJobSeeker1",
                table: "JobJobSeeker1");

            migrationBuilder.DropIndex(
                name: "IX_JobJobSeeker1_SavedJobsJobId",
                table: "JobJobSeeker1");

            migrationBuilder.RenameColumn(
                name: "SavedJobsJobId",
                table: "JobJobSeeker1",
                newName: "Job1JobId");

            migrationBuilder.RenameColumn(
                name: "AppliedJobsJobId",
                table: "JobJobSeeker",
                newName: "JobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobJobSeeker_AppliedJobsJobId",
                table: "JobJobSeeker",
                newName: "IX_JobJobSeeker_JobId");

            migrationBuilder.AddColumn<string>(
                name: "JobSeekerId1",
                table: "Proposals",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobJobSeeker1",
                table: "JobJobSeeker1",
                columns: new[] { "Job1JobId", "SavedById" });

            migrationBuilder.CreateTable(
                name: "JobJobSeeker2",
                columns: table => new
                {
                    AppliedJobsJobId = table.Column<int>(type: "INTEGER", nullable: false),
                    JobSeekerId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobJobSeeker2", x => new { x.AppliedJobsJobId, x.JobSeekerId });
                    table.ForeignKey(
                        name: "FK_JobJobSeeker2_AspNetUsers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobJobSeeker2_Jobs_AppliedJobsJobId",
                        column: x => x.AppliedJobsJobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobJobSeeker3",
                columns: table => new
                {
                    JobSeeker1Id = table.Column<string>(type: "TEXT", nullable: false),
                    SavedJobsJobId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobJobSeeker3", x => new { x.JobSeeker1Id, x.SavedJobsJobId });
                    table.ForeignKey(
                        name: "FK_JobJobSeeker3_AspNetUsers_JobSeeker1Id",
                        column: x => x.JobSeeker1Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobJobSeeker3_Jobs_SavedJobsJobId",
                        column: x => x.SavedJobsJobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_JobSeekerId1",
                table: "Proposals",
                column: "JobSeekerId1");

            migrationBuilder.CreateIndex(
                name: "IX_JobJobSeeker1_SavedById",
                table: "JobJobSeeker1",
                column: "SavedById");

            migrationBuilder.CreateIndex(
                name: "IX_JobJobSeeker2_JobSeekerId",
                table: "JobJobSeeker2",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobJobSeeker3_SavedJobsJobId",
                table: "JobJobSeeker3",
                column: "SavedJobsJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobJobSeeker_Jobs_JobId",
                table: "JobJobSeeker",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobJobSeeker1_Jobs_Job1JobId",
                table: "JobJobSeeker1",
                column: "Job1JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_AspNetUsers_JobSeekerId1",
                table: "Proposals",
                column: "JobSeekerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
