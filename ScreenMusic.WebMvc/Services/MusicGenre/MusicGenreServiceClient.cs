using ScreenMusic.Arguments;

namespace ScreenMusic.WebMvc.Services;

public class MusicGenreServiceClient(IHttpClientFactory factory) : BaseServiceClient<InputCreateMusicGenre, InputUpdateMusicGenre, OutputMusicGenre, InputIdentifierMusicGenre>(factory) { }