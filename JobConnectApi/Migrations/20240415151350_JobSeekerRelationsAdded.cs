using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobConnectApi.Migrations
{
    /// <inheritdoc />
    public partial class JobSeekerRelationsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobJobSeeker",
                columns: table => new
                {
                    ApplicantsId = table.Column<string>(type: "TEXT", nullable: false),
                    JobId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobJobSeeker", x => new { x.ApplicantsId, x.JobId });
                    table.ForeignKey(
                        name: "FK_JobJobSeeker_AspNetUsers_ApplicantsId",
                        column: x => x.ApplicantsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobJobSeeker_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobJobSeeker1",
                columns: table => new
                {
                    Job1JobId = table.Column<int>(type: "INTEGER", nullable: false),
                    SavedById = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobJobSeeker1", x => new { x.Job1JobId, x.SavedById });
                    table.ForeignKey(
                        name: "FK_JobJobSeeker1_AspNetUsers_SavedById",
                        column: x => x.SavedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobJobSeeker1_Jobs_Job1JobId",
                        column: x => x.Job1JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    ProposalId = table.Column<string>(type: "TEXT", nullable: false),
                    JobSeekerId = table.Column<string>(type: "TEXT", nullable: false),
                    JobId = table.Column<int>(type: "INTEGER", nullable: false),
                    CoverLetter = table.Column<string>(type: "TEXT", nullable: false),
                    AttachmentPath = table.Column<string>(type: "TEXT", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    JobId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    JobSeekerId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.ProposalId);
                    table.ForeignKey(
                        name: "FK_Proposals_AspNetUsers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proposals_AspNetUsers_JobSeekerId1",
                        column: x => x.JobSeekerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Proposals_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proposals_Jobs_JobId1",
                        column: x => x.JobId1,
                        principalTable: "Jobs",
                        principalColumn: "JobId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobJobSeeker_JobId",
                table: "JobJobSeeker",
                column: "JobId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_JobId",
                table: "Proposals",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_JobId1",
                table: "Proposals",
                column: "JobId1");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_JobSeekerId",
                table: "Proposals",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_JobSeekerId1",
                table: "Proposals",
                column: "JobSeekerId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobJobSeeker");

            migrationBuilder.DropTable(
                name: "JobJobSeeker1");

            migrationBuilder.DropTable(
                name: "JobJobSeeker2");

            migrationBuilder.DropTable(
                name: "JobJobSeeker3");

            migrationBuilder.DropTable(
                name: "Proposals");
        }
    }
}
