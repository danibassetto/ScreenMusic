using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScreenMusic.Domain.Entities;

namespace ScreenMusic.Infraestructure.Maps;

public class MusicMap : IEntityTypeConfiguration<Music>
{
    public void Configure(EntityTypeBuilder<Music> builder)
    {
        builder.ToTable("musica");

        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Artist).WithMany(x => x.ListMusic).HasForeignKey(x => x.ArtistId).HasPrincipalKey(x => x.Id);
        builder.HasOne(x => x.MusicGenre).WithMany(x => x.ListMusic).HasForeignKey(x => x.MusicGenreId).HasPrincipalKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Id).HasColumnType("BIGINT");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.CreationDate).HasColumnName("data_cadastro");
        builder.Property(x => x.CreationDate).IsRequired();
        builder.Property(x => x.CreationDate).HasColumnType("DATETIME");
        builder.Property(x => x.CreationDate).ValueGeneratedNever();

        builder.Property(x => x.ChangeDate).HasColumnName("data_alteracao");
        builder.Property(x => x.ChangeDate).HasColumnType("DATETIME");
        builder.Property(x => x.ChangeDate).ValueGeneratedNever();

        builder.Property(x => x.Name).HasColumnName("nome");
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(200);
        builder.Property(x => x.Name).HasColumnType("VARCHAR(200)");
        builder.Property(x => x.Name).ValueGeneratedNever();

        builder.Property(x => x.ReleaseYear).HasColumnName("ano_lancamento");
        builder.Property(x => x.ReleaseYear).IsRequired();
        builder.Property(x => x.ReleaseYear).HasColumnType("INT");
        builder.Property(x => x.ReleaseYear).ValueGeneratedNever();

        builder.Property(x => x.ArtistId).HasColumnName("id_artista");
        builder.Property(x => x.ArtistId).IsRequired();
        builder.Property(x => x.ArtistId).HasColumnType("BIGINT");
        builder.Property(x => x.ArtistId).ValueGeneratedNever();

        builder.Property(x => x.MusicGenreId).HasColumnName("id_genero_musical");
        builder.Property(x => x.MusicGenreId).IsRequired();
        builder.Property(x => x.MusicGenreId).HasColumnType("BIGINT");
        builder.Property(x => x.MusicGenreId).ValueGeneratedNever();

        builder.Property(x => x.YoutubeLink).HasColumnName("link_youtube");
        builder.Property(x => x.YoutubeLink).IsRequired();
        builder.Property(x => x.YoutubeLink).HasMaxLength(500);
        builder.Property(x => x.YoutubeLink).HasColumnType("VARCHAR(500)");
        builder.Property(x => x.YoutubeLink).ValueGeneratedNever();
    }
}