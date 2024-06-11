using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;
using ScreenMusic.Domain.Interfaces.Service;

namespace ScreenMusic.Domain.Services;

public class MusicService(IMusicRepository repository) : BaseService<IMusicRepository, InputCreateMusic, InputUpdateMusic, Music, OutputMusic, InputIdentifierMusic>(repository), IMusicService 
{
    public List<OutputMusic>? GetListByArtistId(long artistId)
    {
        var listMusic = _repository!.GetListByArtistId(artistId);
        
        if (listMusic is not null)
            return FromEntityToOutput(listMusic);
        else
            return default;
    }

    public List<OutputMusic>? GetListByMusicGenreId(long musicGenreId)
    {
        var listMusic = _repository!.GetListByArtistId(musicGenreId);

        if (listMusic is not null)
            return FromEntityToOutput(listMusic);
        else
            return default;
    }
}