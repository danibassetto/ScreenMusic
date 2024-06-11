using ScreenMusic.Arguments;

namespace ScreenMusic.WebMvc.Services;

public class MusicServiceClient(IHttpClientFactory factory) : BaseServiceClient<InputCreateMusic, InputUpdateMusic, OutputMusic, InputIdentifierMusic>(factory) { }