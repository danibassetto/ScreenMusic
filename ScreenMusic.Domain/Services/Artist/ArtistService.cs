using Microsoft.Extensions.Hosting;
using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;
using ScreenMusic.Domain.Interfaces.Service;

namespace ScreenMusic.Domain.Services;

public class ArtistService(IArtistRepository repository, IHostEnvironment hostEnviroment, IMusicRepository musicRepository) : BaseService<IArtistRepository, InputCreateArtist, InputUpdateArtist, Artist, OutputArtist, InputIdentifierArtist>(repository), IArtistService
{
    private readonly IHostEnvironment _hostEnviroment = hostEnviroment;
    private readonly IMusicRepository _musicRepository = musicRepository;

    public override long? Create(InputCreateArtist inputCreate)
    {
        Artist? originalArtist = _repository!.GetByIdentifier(new InputIdentifierArtist(inputCreate.Name));

        if(originalArtist is not null)
            throw new InvalidOperationException($"Artista com o nome '{inputCreate.Name}' já existe.");

        var name = inputCreate.Name.Trim();
        var biography = inputCreate.Biography.Trim();
        var profilePhoto = DateTime.Now.ToString("ddMMyyyyhhss") + "." + name + ".jpeg";

        var path = Path.Combine(_hostEnviroment.ContentRootPath, "wwwroot", "ProfilePhotos", profilePhoto);

        using MemoryStream ms = new(Convert.FromBase64String(inputCreate.ProfilePhoto));
        using FileStream fs = new(path, FileMode.Create);
        ms.CopyTo(fs);

        Artist artist = new(name, $"/ProfilePhotos/{profilePhoto}", biography);

        return _repository!.Create(artist);
    }

    public override bool Delete(long id)
    {
        var entity = Get(id) ?? throw new KeyNotFoundException("Id inválido ou inexistente. Processo: Delete");

        var musicRelation = (from i in _musicRepository.GetListByArtistId(id) select i).Any();

        if(musicRelation)
            throw new InvalidOperationException("Não é possível excluir o artista porque existem músicas relacionadas a ele.");

        _repository!.Delete(FromOutputToEntity(entity));
        return true;
    }
}