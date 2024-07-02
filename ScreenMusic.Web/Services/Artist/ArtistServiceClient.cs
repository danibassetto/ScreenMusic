using Newtonsoft.Json;
using ScreenMusic.Arguments;
using System.Net.Http.Json;

namespace ScreenMusic.Web.Services;

public class ArtistServiceClient(IHttpClientFactory factory) : BaseServiceClient<InputCreateArtist, InputUpdateArtist, OutputArtist, InputIdentifierArtist>(factory) 
{
    public async Task<bool> Review(long id, InputReviewArtist inputReviewArtist)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/{NameService}/Review/{id}", inputReviewArtist);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (HttpRequestException)
        {
            return false;
        }
    }

    public async Task<OutputArtistReview?> GetReview(long id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"api/{NameService}/GetReview/{id}");

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            OutputArtistReview? outputArtistReview = JsonConvert.DeserializeObject<BaseResponseApi<OutputArtistReview>>(content)!.Result;

            return outputArtistReview;
        }
        else
            return default;
    }
}