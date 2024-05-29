using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;

namespace ScreenMusic.Infraestructure.Repository;

public class ArtistRepository(ScreenMusicContext context) : BaseRepository<Artist, InputIdentifierArtist>(context), IArtistRepository { }