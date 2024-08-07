﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScreenMusic.Domain.Entities;

namespace ScreenMusic.Infraestructure.Maps;

public class ArtistMap : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.ToTable("artista");
        builder.HasMany(x => x.ListMusic).WithOne(x => x.Artist).HasForeignKey(x => x.ArtistId).HasPrincipalKey(x => x.Id);
        builder.HasMany(x => x.ListArtistReview).WithOne(x => x.Artist).HasForeignKey(x => x.ArtistId).HasPrincipalKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);

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
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Name).HasColumnType("VARCHAR(100)");
        builder.Property(x => x.Name).ValueGeneratedNever();

        builder.Property(x => x.ProfilePhoto).HasColumnName("foto_perfil");
        builder.Property(x => x.ProfilePhoto).HasMaxLength(1000);
        builder.Property(x => x.ProfilePhoto).HasColumnType("VARCHAR(1000)");
        builder.Property(x => x.ProfilePhoto).ValueGeneratedNever();

        builder.Property(x => x.Biography).HasColumnName("biografia");
        builder.Property(x => x.Biography).IsRequired();
        builder.Property(x => x.Biography).HasMaxLength(500);
        builder.Property(x => x.Biography).HasColumnType("VARCHAR(500)");
        builder.Property(x => x.Biography).ValueGeneratedNever();
    }
}