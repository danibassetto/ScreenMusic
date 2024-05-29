using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScreenMusic.Domain.Entities;

namespace ScreenMusic.Infraestructure.Maps;

public class MusicGenreMap : IEntityTypeConfiguration<MusicGenre>
{
    public void Configure(EntityTypeBuilder<MusicGenre> builder)
    {
        builder.ToTable("genero_musical");

        builder.HasKey(x => x.Id);

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

        builder.Property(x => x.Description).HasColumnName("descricao");
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(150);
        builder.Property(x => x.Description).HasColumnType("VARCHAR(150)");
        builder.Property(x => x.Description).ValueGeneratedNever();
    }
}