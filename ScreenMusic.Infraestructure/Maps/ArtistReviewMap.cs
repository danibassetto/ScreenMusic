using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScreenMusic.Domain.Entities;

namespace ScreenMusic.Infraestructure.Maps;

public class ArtistReviewMap : IEntityTypeConfiguration<ArtistReview>
{
    public void Configure(EntityTypeBuilder<ArtistReview> builder)
    {
        builder.ToTable("avaliacao_artista");

        builder.HasKey(a => new { a.ArtistId, a.UserId });

        builder.HasOne(x => x.Artist).WithMany(x => x.ListArtistReview).HasForeignKey(x => x.ArtistId).HasPrincipalKey(x => x.Id);

        builder.Property(x => x.ArtistId).HasColumnName("id_artista");
        builder.Property(x => x.ArtistId).IsRequired();
        builder.Property(x => x.ArtistId).HasColumnType("BIGINT");
        builder.Property(x => x.ArtistId).ValueGeneratedNever();

        builder.Property(x => x.UserId).HasColumnName("id_usuario");
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.UserId).HasColumnType("BIGINT");
        builder.Property(x => x.UserId).ValueGeneratedNever();

        builder.Property(x => x.Rating).HasColumnName("classificacao");
        builder.Property(x => x.Rating).IsRequired();
        builder.Property(x => x.Rating).HasColumnType("INT");
        builder.Property(x => x.Rating).ValueGeneratedNever();
    }
}