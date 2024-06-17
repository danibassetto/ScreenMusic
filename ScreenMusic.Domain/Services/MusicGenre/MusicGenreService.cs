using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;
using ScreenMusic.Domain.Interfaces.Service;

namespace ScreenMusic.Domain.Services;

public class MusicGenreService(IMusicGenreRepository repository, IMusicRepository musicRepository) : BaseService<IMusicGenreRepository, InputCreateMusicGenre, InputUpdateMusicGenre, MusicGenre, OutputMusicGenre, InputIdentifierMusicGenre>(repository), IMusicGenreService
{
    private readonly IMusicRepository _musicRepository = musicRepository;

    public override long? Create(InputCreateMusicGenre inputCreate)
    {
        MusicGenre? originalMusicGenre = _repository!.GetByIdentifier(new InputIdentifierMusicGenre(inputCreate.Name));

        if (originalMusicGenre is not null)
            throw new InvalidOperationException($"Gênero Musical com o nome '{inputCreate.Name}' já existe.");

        var name = inputCreate.Name.Trim();
        var description = inputCreate.Description!.Trim();

        MusicGenre musicGenre = new(name, description);

        return _repository!.Create(musicGenre);
    }

    public override bool Delete(long id)
    {
        var entity = Get(id) ?? throw new KeyNotFoundException("Id inválido ou inexistente. Processo: Delete");

        var musicRelation = (from i in _musicRepository.GetListByMusicGenreId(id) select i).Any();

        if (musicRelation)
            throw new InvalidOperationException("Não é possível excluir o gênero musical porque existem músicas relacionadas a ele.");

        _repository!.Delete(FromOutputToEntity(entity));
        return true;
    }
}