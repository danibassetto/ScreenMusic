using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;

namespace ScreenMusic.Infraestructure.Repository;

public class MusicRepository(ScreenMusicContext context) : BaseRepository<Music, InputIdentifierMusic>(context), IMusicRepository { }