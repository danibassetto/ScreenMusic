using ScreenMusic.Arguments;

namespace ScreenMusic.Web.Services;

public class ArtistServiceClient(IHttpClientFactory factory) : BaseServiceClient<InputCreateArtist, InputUpdateArtist, OutputArtist, InputIdentifierArtist>(factory) { }