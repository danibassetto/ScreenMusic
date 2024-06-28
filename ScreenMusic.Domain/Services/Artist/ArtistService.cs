using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;
using ScreenMusic.Domain.Interfaces.Service;
using System.Security.Claims;

namespace ScreenMusic.Domain.Services;

public class ArtistService(IArtistRepository repository, IHostEnvironment hostEnviroment, IHttpContextAccessor httpContextAccessor, UserManager<User> userRepository, IMusicRepository musicRepository, IArtistReviewRepository artistReviewRepository) : BaseService<IArtistRepository, InputCreateArtist, InputUpdateArtist, Artist, OutputArtist, InputIdentifierArtist>(repository), IArtistService
{
    private readonly IHostEnvironment _hostEnviroment = hostEnviroment;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<User> _userRepository = userRepository;
    private readonly IMusicRepository _musicRepository = musicRepository;
    private readonly IArtistReviewRepository _artistReviewRepository = artistReviewRepository;

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

        if (!string.IsNullOrEmpty(inputUpdate.ProfilePhoto))
        {
            DeletePhoto(output.ProfilePhoto!);

            var profilePhoto = "artista_" + output.Name + ".jpeg";
            var path = Path.Combine(_hostEnviroment.ContentRootPath, "wwwroot", "ProfilePhotos", profilePhoto);

            using MemoryStream ms = new(Convert.FromBase64String(inputUpdate.ProfilePhoto!));
            using FileStream fs = new(path, FileMode.Create);
            ms.CopyTo(fs);

            output.ProfilePhoto = $"/ProfilePhotos/{profilePhoto}";
        }

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

    public bool Review(long id, InputReviewArtist inputReviewArtist)
    {
        var outputArtist = Get(id) ?? throw new KeyNotFoundException("Id inválido ou inexistente. Processo: Review");

        var email = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? throw new InvalidOperationException("Usuário não conectado");

        var user = _userRepository.FindByEmailAsync(email).Result ?? throw new InvalidOperationException("Usuário não encontrado.");

        ArtistReview? artistReview = _artistReviewRepository.GetByIdentifier(new InputIdentifierArtistReview(outputArtist.Id, user.Id));

        if (artistReview is null)
            _artistReviewRepository.Create(new ArtistReview(outputArtist.Id, user.Id, Math.Min(Math.Max(inputReviewArtist.Rating, 1), 5)));
        else
        {
            artistReview.Rating = Math.Min(Math.Max(inputReviewArtist.Rating, 1), 5);
            _artistReviewRepository.Update(artistReview);
        }

        return true;
    }

    public OutputArtistReview? GetReview(long id)
    {
        var entity = Get(id) ?? throw new KeyNotFoundException("Id inválido ou inexistente. Processo: GetReview");

        var email = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? throw new InvalidOperationException("Usuário não conectado");

        var user = _userRepository.FindByEmailAsync(email).Result ?? throw new InvalidOperationException("Usuário não encontrado.");

        var review = entity.ListArtistReview?.FirstOrDefault(a => a.ArtistId == entity.Id && a.UserId == user.Id);

        if (review is not null)
            return review;

        return default;
    }

    private void DeletePhoto(string profilePhoto)
    {
        var profilePhotoPath = Path.Combine(_hostEnviroment.ContentRootPath, "wwwroot", profilePhoto.TrimStart('/'));
        if (File.Exists(profilePhotoPath))
            File.Delete(profilePhotoPath);
    }
}