using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenMusic.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldYoutubeLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "link_youtube",
                table: "musica",
                type: "VARCHAR(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "link_youtube",
                table: "musica");
        }
    }
}
