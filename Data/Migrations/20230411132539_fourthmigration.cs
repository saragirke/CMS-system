using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cmsSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class fourthmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SerivceTitle",
                table: "Service",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "AboutService",
                table: "Service",
                newName: "Description");

            migrationBuilder.CreateTable(
                name: "Start",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ImageName = table.Column<string>(type: "TEXT", nullable: true),
                    AltText = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Start", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Widget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WidgetTitle = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    WidgetText = table.Column<string>(type: "TEXT", nullable: false),
                    WidgetColor = table.Column<string>(type: "TEXT", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    ImageName = table.Column<string>(type: "TEXT", nullable: true),
                    AltText = table.Column<string>(type: "TEXT", nullable: true),
                    Display = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Widget", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Start");

            migrationBuilder.DropTable(
                name: "Widget");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Service",
                newName: "SerivceTitle");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Service",
                newName: "AboutService");
        }
    }
}
