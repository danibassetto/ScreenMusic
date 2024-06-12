using Microsoft.EntityFrameworkCore;
using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;

namespace ScreenMusic.Infraestructure.Repository;

public class MusicRepository(ScreenMusicContext context) : BaseRepository<Music, InputIdentifierMusic>(context), IMusicRepository
{
    public List<Music>? GetListByArtistId(long artistId)
    {
        IQueryable<Music> query = _context.Set<Music>().Where(x => x.ArtistId == artistId).AsNoTracking();
        query = IncludeVirtualProperties(query);
        return [.. query];
    }

    public List<Music>? GetListByMusicGenreId(long musicGenreId)
    {
        IQueryable<Music> query = _context.Set<Music>().Where(x => x.MusicGenreId == musicGenreId).AsNoTracking();
        query = IncludeVirtualProperties(query);
        return [.. query];
    }
}