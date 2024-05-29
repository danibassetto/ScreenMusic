using Microsoft.EntityFrameworkCore;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Infraestructure.Maps;

namespace ScreenMusic.Infraestructure;

public class ScreenMusicContext(DbContextOptions<ScreenMusicContext> options) : DbContext(options)
{
    public DbSet<Artist> Artist { get; set; }
    public DbSet<Music> Music { get; set; }
    public DbSet<MusicGenre> MusicGenre { get; set; }
    public DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ArtistMap());
        modelBuilder.ApplyConfiguration(new MusicMap());
        modelBuilder.ApplyConfiguration(new MusicGenreMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        base.OnModelCreating(modelBuilder);
    }
}