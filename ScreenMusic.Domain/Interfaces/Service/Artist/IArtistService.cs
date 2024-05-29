using ScreenMusic.Arguments;

namespace ScreenMusic.Domain.Interfaces.Service;

public interface IArtistService : IBaseService<InputCreateArtist, InputUpdateArtist, OutputArtist, InputIdentifierArtist> { }