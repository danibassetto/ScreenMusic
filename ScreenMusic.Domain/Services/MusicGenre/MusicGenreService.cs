using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;
using ScreenMusic.Domain.Interfaces.Service;

namespace ScreenMusic.Domain.Services;

public class MusicGenreService(IMusicGenreRepository repository) : BaseService_1<IMusicGenreRepository, InputCreateMusicGenre, InputUpdateMusicGenre, MusicGenre, OutputMusicGenre, InputIdentifierMusicGenre>(repository), IMusicGenreService { }