using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;
using ScreenMusic.Domain.Interfaces.Service;

namespace ScreenMusic.Domain.Services;

public class MusicService(IMusicRepository repository) : BaseService<IMusicRepository, InputCreateMusic, InputUpdateMusic, Music, OutputMusic, InputIdentifierMusic>(repository), IMusicService
{
    public override long? Create(InputCreateMusic inputCreate)
    {
        if(inputCreate.ReleaseYear > DateTime.Now.Year)
            throw new InvalidOperationException($"Ano de lançamento inválido!");

        return _repository!.Create(FromInputCreateToEntity(inputCreate));
    }

    public override long? Update(long id, InputUpdateMusic inputUpdate)
    {
        var output = Get(id) ?? throw new KeyNotFoundException("Id inválido ou inexistente. Processo: Update");

        if (inputUpdate.ReleaseYear > DateTime.Now.Year)
            throw new InvalidOperationException($"Ano de lançamento inválido!");

        output.ArtistId = inputUpdate.ArtistId!.Value;
        output.MusicGenreId = inputUpdate.ArtistId!.Value;
        output.ReleaseYear = inputUpdate.ReleaseYear!.Value;
        output.Name = inputUpdate.Name;
        output.YoutubeLink = inputUpdate.YoutubeLink;

        return _repository!.Update(FromOutputToEntity(output));
    }

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