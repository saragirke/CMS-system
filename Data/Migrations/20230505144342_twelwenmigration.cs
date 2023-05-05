using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cmsSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class twelwenmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comment_NewsId",
                table: "Comment");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_NewsId",
                table: "Comment",
                column: "NewsId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comment_NewsId",
                table: "Comment");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_NewsId",
                table: "Comment",
                column: "NewsId");
        }
    }
}
