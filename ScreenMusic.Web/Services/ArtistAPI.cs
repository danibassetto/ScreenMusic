using Newtonsoft.Json;
using ScreenMusic.Arguments;
using System.Net.Http.Json;

namespace ScreenMusic.Web.Services;

public class ArtistAPI(IHttpClientFactory factory)
{
    private readonly HttpClient _httpClient = factory.CreateClient("API");

    public async Task<ICollection<OutputArtist>?> GetAll()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<OutputArtist>>("Artist");
    }

    public async Task<OutputArtist?> GetByName(InputIdentifierArtist inputIdentifierArtist)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Artist", inputIdentifierArtist);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            OutputArtist? outputArtist = JsonConvert.DeserializeObject<OutputArtist>(content);

            return outputArtist;
        }
        else
            return null;
    }

    public async Task Create(InputCreateArtist inputCreateArtist)
    {
        await _httpClient.PostAsJsonAsync("Artist", inputCreateArtist);
    }

    public async Task Delete(long id)
    {
        await _httpClient.DeleteAsync($"Artist/{id}");
    }

    public async Task Update(long id, InputUpdateArtist inputUpdateArtist)
    {
        await _httpClient.PutAsJsonAsync($"Artist/{id}", inputUpdateArtist);
    }
}