using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScreenMusic.Domain.Entities;

namespace ScreenMusic.Infraestructure.Maps;

public class ArtistReviewMap : IEntityTypeConfiguration<ArtistReview>
{
    public void Configure(EntityTypeBuilder<ArtistReview> builder)
    {
        builder.ToTable("avaliacao_artista");

        builder.HasOne(x => x.Artist).WithMany(x => x.ListArtistReview).HasForeignKey(x => x.ArtistId);
        builder.HasOne(x => x.User).WithMany(x => x.ListArtistReview).HasForeignKey(x => x.UserId);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Id).HasColumnType("BIGINT");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.ArtistId).HasColumnName("id_artista");
        builder.Property(x => x.ArtistId).IsRequired();
        builder.Property(x => x.ArtistId).HasColumnType("BIGINT");

        builder.Property(x => x.UserId).HasColumnName("id_usuario");
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.UserId).HasColumnType("BIGINT");

        builder.Property(x => x.Rating).HasColumnName("classificacao");
        builder.Property(x => x.Rating).IsRequired();
        builder.Property(x => x.Rating).HasColumnType("INT");
    }
}