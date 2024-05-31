using ScreenMusic.Arguments;

namespace ScreenMusic.Web.Services;

public class MusicServiceClient(IHttpClientFactory factory) : BaseServiceClient<InputCreateMusic, InputUpdateMusic, OutputMusic, InputIdentifierMusic>(factory) { }