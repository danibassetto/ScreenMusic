using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;
using ScreenMusic.Domain.Interfaces.Service;

namespace ScreenMusic.Domain.Services;

public class ArtistService(IArtistRepository repository) : BaseService_1<IArtistRepository, InputCreateArtist, InputUpdateArtist, Artist, OutputArtist, InputIdentifierArtist>(repository), IArtistService { }