using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace JobConnectApi.Migrations
{
    /// <inheritdoc />
    public partial class JobsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EmployerId1 = table.Column<string>(type: "varchar(255)", nullable: true),
                    AdminId = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Jobs_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_AspNetUsers_EmployerId1",
                        column: x => x.EmployerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_AdminId",
                table: "Jobs",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_EmployerId1",
                table: "Jobs",
                column: "EmployerId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
