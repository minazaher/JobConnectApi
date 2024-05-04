using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobConnectApi.Migrations
{
    /// <inheritdoc />
    public partial class MessageColumnsRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Messages",
                newName: "SenderName");

            migrationBuilder.RenameColumn(
                name: "RecipientId",
                table: "Messages",
                newName: "RecipientName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderName",
                table: "Messages",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "RecipientName",
                table: "Messages",
                newName: "RecipientId");
        }
    }
}
