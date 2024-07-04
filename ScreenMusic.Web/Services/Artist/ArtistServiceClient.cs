using ScreenMusic.Arguments;
using ScreenMusic.Web.Services.Base;

namespace ScreenMusic.Web.Services;

public class ArtistServiceClient(IHttpClientFactory factory) : BaseServiceClient<InputCreateArtist, InputUpdateArtist, OutputArtist, InputIdentifierArtist>(factory)
{
    public async Task<BaseServiceClientResponse<bool>> Review(long id, InputReviewArtist inputReviewArtist)
    {
        return await HandleRequestAsync<bool>(HttpMethod.Post, $"{_nameService}/Review/{id}", inputReviewArtist);
    }

    public async Task<BaseServiceClientResponse<OutputArtistReview>> GetReview(long id)
    {
        return await HandleRequestAsync<OutputArtistReview>(HttpMethod.Get, $"{_nameService}/GetReview/{id}", null);
    }
}