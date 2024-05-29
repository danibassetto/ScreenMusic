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
        builder.HasOne(x => x.Artist).WithMany(x => x.ListMusic).HasForeignKey(x => x.ArtistaId).HasPrincipalKey(x => x.Id);
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
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.Name).HasColumnType("VARCHAR(50)");
        builder.Property(x => x.Name).ValueGeneratedNever();

        builder.Property(x => x.ReleaseYear).HasColumnName("ano_lancamento");
        builder.Property(x => x.ReleaseYear).IsRequired();
        builder.Property(x => x.ReleaseYear).HasColumnType("INT");
        builder.Property(x => x.ReleaseYear).ValueGeneratedNever();

        builder.Property(x => x.ArtistaId).HasColumnName("id_artista");
        builder.Property(x => x.ArtistaId).IsRequired();
        builder.Property(x => x.ArtistaId).HasColumnType("BIGINT");
        builder.Property(x => x.ArtistaId).ValueGeneratedNever();

        builder.Property(x => x.MusicGenreId).HasColumnName("id_genero_musical");
        builder.Property(x => x.MusicGenreId).IsRequired();
        builder.Property(x => x.MusicGenreId).HasColumnType("BIGINT");
        builder.Property(x => x.MusicGenreId).ValueGeneratedNever();
    }
}