using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;

namespace ScreenMusic.Infraestructure.Repository;

public class MusicGenreRepository(ScreenMusicContext context) : BaseRepository<MusicGenre, InputIdentifierMusicGenre>(context), IMusicGenreRepository { }