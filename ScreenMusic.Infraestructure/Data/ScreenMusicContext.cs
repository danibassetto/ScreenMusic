using Microsoft.EntityFrameworkCore;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Infraestructure.Maps;

namespace ScreenMusic.Infraestructure;

public class ScreenMusicContext(DbContextOptions<ScreenMusicContext> options) : DbContext(options)
{
    public DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        base.OnModelCreating(modelBuilder);
    }
}