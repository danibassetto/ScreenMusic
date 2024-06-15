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
        Artist? originalArtist = _repository!.GetByIdentifier(new InputIdentifierArtist(inputCreate.Name!));

        if (originalArtist is not null)
            throw new InvalidOperationException($"Artista com o nome '{inputCreate.Name}' já existe.");

        var name = inputCreate.Name!.Trim();
        var biography = inputCreate.Biography!.Trim();
        var profilePhoto = "artista_" + name + ".jpeg";

        var path = Path.Combine(_hostEnviroment.ContentRootPath, "wwwroot", "ProfilePhotos", profilePhoto);

        using MemoryStream ms = new(Convert.FromBase64String(inputCreate.ProfilePhoto!));
        using FileStream fs = new(path, FileMode.Create);
        ms.CopyTo(fs);

        Artist artist = new(name, $"/ProfilePhotos/{profilePhoto}", biography);

        return _repository!.Create(artist);
    }

    public override long? Update(long id, InputUpdateArtist inputUpdate)
    {
        var output = Get(id) ?? throw new KeyNotFoundException("Id inválido ou inexistente. Processo: Update");

        DeletePhoto(output.ProfilePhoto!);

        var profilePhoto = "artista_" + output.Name + ".jpeg";
        var path = Path.Combine(_hostEnviroment.ContentRootPath, "wwwroot", "ProfilePhotos", profilePhoto);

        using MemoryStream ms = new(Convert.FromBase64String(inputUpdate.ProfilePhoto!));
        using FileStream fs = new(path, FileMode.Create);
        ms.CopyTo(fs);

        output.ProfilePhoto = $"/ProfilePhotos/{profilePhoto}";
        output.Biography = inputUpdate.Biography;

        return _repository!.Update(FromOutputToEntity(output));
    }

    public override bool Delete(long id)
    {
        var output = Get(id) ?? throw new KeyNotFoundException("Id inválido ou inexistente. Processo: Delete");

        var musicRelation = (from i in _musicRepository.GetListByArtistId(id) select i).Any();

        if (musicRelation)
            throw new InvalidOperationException("Não é possível excluir o artista porque existem músicas relacionadas a ele.");

        DeletePhoto(output.ProfilePhoto!);

        _repository!.Delete(FromOutputToEntity(output));
        return true;
    }

    private void DeletePhoto(string profilePhoto)
    {
        var profilePhotoPath = Path.Combine(_hostEnviroment.ContentRootPath, "wwwroot", profilePhoto.TrimStart('/'));
        if (File.Exists(profilePhotoPath))
            File.Delete(profilePhotoPath);
    }
}