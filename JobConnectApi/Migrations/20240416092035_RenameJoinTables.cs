using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobConnectApi.Migrations
{
    /// <inheritdoc />
    public partial class RenameJoinTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobJobSeeker_AspNetUsers_ApplicantsId",
                table: "JobJobSeeker");

            migrationBuilder.DropForeignKey(
                name: "FK_JobJobSeeker_Jobs_AppliedJobsJobId",
                table: "JobJobSeeker");

            migrationBuilder.DropForeignKey(
                name: "FK_JobJobSeeker1_AspNetUsers_SavedById",
                table: "JobJobSeeker1");

            migrationBuilder.DropForeignKey(
                name: "FK_JobJobSeeker1_Jobs_SavedJobsJobId",
                table: "JobJobSeeker1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobJobSeeker1",
                table: "JobJobSeeker1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobJobSeeker",
                table: "JobJobSeeker");

            migrationBuilder.RenameTable(
                name: "JobJobSeeker1",
                newName: "SavedJobs");

            migrationBuilder.RenameTable(
                name: "JobJobSeeker",
                newName: "JobApplications");

            migrationBuilder.RenameIndex(
                name: "IX_JobJobSeeker1_SavedJobsJobId",
                table: "SavedJobs",
                newName: "IX_SavedJobs_SavedJobsJobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobJobSeeker_AppliedJobsJobId",
                table: "JobApplications",
                newName: "IX_JobApplications_AppliedJobsJobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedJobs",
                table: "SavedJobs",
                columns: new[] { "SavedById", "SavedJobsJobId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobApplications",
                table: "JobApplications",
                columns: new[] { "ApplicantsId", "AppliedJobsJobId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_AspNetUsers_ApplicantsId",
                table: "JobApplications",
                column: "ApplicantsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Jobs_AppliedJobsJobId",
                table: "JobApplications",
                column: "AppliedJobsJobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedJobs_AspNetUsers_SavedById",
                table: "SavedJobs",
                column: "SavedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedJobs_Jobs_SavedJobsJobId",
                table: "SavedJobs",
                column: "SavedJobsJobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_AspNetUsers_ApplicantsId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Jobs_AppliedJobsJobId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedJobs_AspNetUsers_SavedById",
                table: "SavedJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedJobs_Jobs_SavedJobsJobId",
                table: "SavedJobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedJobs",
                table: "SavedJobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobApplications",
                table: "JobApplications");

            migrationBuilder.RenameTable(
                name: "SavedJobs",
                newName: "JobJobSeeker1");

            migrationBuilder.RenameTable(
                name: "JobApplications",
                newName: "JobJobSeeker");

            migrationBuilder.RenameIndex(
                name: "IX_SavedJobs_SavedJobsJobId",
                table: "JobJobSeeker1",
                newName: "IX_JobJobSeeker1_SavedJobsJobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_AppliedJobsJobId",
                table: "JobJobSeeker",
                newName: "IX_JobJobSeeker_AppliedJobsJobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobJobSeeker1",
                table: "JobJobSeeker1",
                columns: new[] { "SavedById", "SavedJobsJobId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobJobSeeker",
                table: "JobJobSeeker",
                columns: new[] { "ApplicantsId", "AppliedJobsJobId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JobJobSeeker_AspNetUsers_ApplicantsId",
                table: "JobJobSeeker",
                column: "ApplicantsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobJobSeeker_Jobs_AppliedJobsJobId",
                table: "JobJobSeeker",
                column: "AppliedJobsJobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobJobSeeker1_AspNetUsers_SavedById",
                table: "JobJobSeeker1",
                column: "SavedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobJobSeeker1_Jobs_SavedJobsJobId",
                table: "JobJobSeeker1",
                column: "SavedJobsJobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
