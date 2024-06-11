using ScreenMusic.Arguments;

namespace ScreenMusic.WebMvc.Services;

public class ArtistServiceClient(IHttpClientFactory factory) : BaseServiceClient<InputCreateArtist, InputUpdateArtist, OutputArtist, InputIdentifierArtist>(factory) { }