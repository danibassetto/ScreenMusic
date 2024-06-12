using ScreenMusic.Arguments;

namespace ScreenMusic.Domain.Interfaces.Service;

public interface IMusicService : IBaseService<InputCreateMusic, InputUpdateMusic, OutputMusic, InputIdentifierMusic>
{
    List<OutputMusic>? GetListByArtistId(long artistId);
    List<OutputMusic>? GetListByMusicGenreId(long musicGenreId);
}