using Microsoft.Extensions.Hosting;
using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;
using ScreenMusic.Domain.Interfaces.Service;

namespace ScreenMusic.Domain.Services;

public class ArtistService(IArtistRepository repository, IHostEnvironment hostEnviroment) : BaseService<IArtistRepository, InputCreateArtist, InputUpdateArtist, Artist, OutputArtist, InputIdentifierArtist>(repository), IArtistService 
{
    private readonly IHostEnvironment _hostEnviroment = hostEnviroment;

    public override long? Create(InputCreateArtist inputCreate)
    {
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
}