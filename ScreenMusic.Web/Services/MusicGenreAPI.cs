using Newtonsoft.Json;
using ScreenMusic.Arguments;
using System.Net.Http.Json;

namespace ScreenMusic.Web.Services;

public class MusicGenreAPI(IHttpClientFactory factory)
{
    private readonly HttpClient _httpClient = factory.CreateClient("API");

    public async Task<List<OutputMusicGenre>?> GetAll()
    {
        return await _httpClient.GetFromJsonAsync<List<OutputMusicGenre>>("MusicGenre");
    }

    public async Task<OutputMusicGenre?> GetByName(InputIdentifierMusicGenre inputIdentifierMusicGenre)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("MusicGenre", inputIdentifierMusicGenre);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            OutputMusicGenre? outputMusicGenre = JsonConvert.DeserializeObject<OutputMusicGenre>(content);

            return outputMusicGenre;
        }
        else
            return null;
    }
}