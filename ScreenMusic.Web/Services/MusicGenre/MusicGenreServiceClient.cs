using ScreenMusic.Arguments;

namespace ScreenMusic.Web.Services;

public class MusicGenreServiceClient(IHttpClientFactory factory) : BaseServiceClient<InputCreateMusicGenre, InputUpdateMusicGenre, OutputMusicGenre, InputIdentifierMusicGenre>(factory) { }