using ScreenMusic.Arguments;

namespace ScreenMusic.Domain.Interfaces.Service;

public interface IMusicGenreService : IBaseService<InputCreateMusicGenre, InputUpdateMusicGenre, OutputMusicGenre, InputIdentifierMusicGenre> { }