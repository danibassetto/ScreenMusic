using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Infraestructure.Maps;

namespace ScreenMusic.Infraestructure;

public class ScreenMusicContext : IdentityDbContext<User, UserRole, long>
{
    public DbSet<Artist> Artist { get; set; }
    public DbSet<Music> Music { get; set; }
    public DbSet<MusicGenre> MusicGenre { get; set; }

    public ScreenMusicContext()    {    }

    public ScreenMusicContext(DbContextOptions options) : base(options)    {    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ArtistMap());
        modelBuilder.ApplyConfiguration(new MusicMap());
        modelBuilder.ApplyConfiguration(new MusicGenreMap());
        base.OnModelCreating(modelBuilder);
    }
}