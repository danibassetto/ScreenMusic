using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenMusic.Api.Migrations;

public partial class InitialMigration : Migration
{

    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "artista",
            columns: table => new
            {
                id = table.Column<long>(type: "BIGINT", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                nome = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                foto_perfil = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                biografia = table.Column<string>(type: "VARCHAR(350)", maxLength: 350, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                data_cadastro = table.Column<DateTime>(type: "DATETIME", nullable: false),
                data_alteracao = table.Column<DateTime>(type: "DATETIME", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_artista", x => x.id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "genero_musical",
            columns: table => new
            {
                id = table.Column<long>(type: "BIGINT", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                nome = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                descricao = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                data_cadastro = table.Column<DateTime>(type: "DATETIME", nullable: false),
                data_alteracao = table.Column<DateTime>(type: "DATETIME", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_genero_musical", x => x.id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "usuario",
            columns: table => new
            {
                id = table.Column<long>(type: "BIGINT", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                username = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                senha = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                data_expiracao_token = table.Column<DateTime>(type: "DATETIME", nullable: true),
                data_cadastro = table.Column<DateTime>(type: "DATETIME", nullable: false),
                data_alteracao = table.Column<DateTime>(type: "DATETIME", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_usuario", x => x.id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "musica",
            columns: table => new
            {
                id = table.Column<long>(type: "BIGINT", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                nome = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                ano_lancamento = table.Column<int>(type: "INT", nullable: false),
                id_artista = table.Column<long>(type: "BIGINT", nullable: false),
                id_genero_musical = table.Column<long>(type: "BIGINT", nullable: false),
                data_cadastro = table.Column<DateTime>(type: "DATETIME", nullable: false),
                data_alteracao = table.Column<DateTime>(type: "DATETIME", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_musica", x => x.id);
                table.ForeignKey(
                    name: "FK_musica_artista_id_artista",
                    column: x => x.id_artista,
                    principalTable: "artista",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_musica_genero_musical_id_genero_musical",
                    column: x => x.id_genero_musical,
                    principalTable: "genero_musical",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_musica_id_artista",
            table: "musica",
            column: "id_artista");

        migrationBuilder.CreateIndex(
            name: "IX_musica_id_genero_musical",
            table: "musica",
            column: "id_genero_musical");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "musica");

        migrationBuilder.DropTable(
            name: "usuario");

        migrationBuilder.DropTable(
            name: "artista");

        migrationBuilder.DropTable(
            name: "genero_musical");
    }
}