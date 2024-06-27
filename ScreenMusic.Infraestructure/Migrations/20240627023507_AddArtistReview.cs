using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenMusic.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddArtistReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "avaliacao_artista",
                columns: table => new
                {
                    id_artista = table.Column<long>(type: "BIGINT", nullable: false),
                    id_usuario = table.Column<long>(type: "BIGINT", nullable: false),
                    classificacao = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_avaliacao_artista", x => new { x.id_artista, x.id_usuario });
                    table.ForeignKey(
                        name: "FK_avaliacao_artista_AspNetUsers_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_avaliacao_artista_artista_id_artista",
                        column: x => x.id_artista,
                        principalTable: "artista",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_avaliacao_artista_id_usuario",
                table: "avaliacao_artista",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "avaliacao_artista");
        }
    }
}
