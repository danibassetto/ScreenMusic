using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;

namespace ScreenMusic.Domain.Interfaces.Repository;

public interface IMusicRepository : IBaseRepository<Music, InputIdentifierMusic> 
{
    List<Music>? GetListByArtistId(long artistId);
    List<Music>? GetListByMusicGenreId(long musicGenreId);
}