using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cmsSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class socials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Header",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Linkedin",
                table: "Header",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Linkedin",
                table: "Header");
        }
    }
}
