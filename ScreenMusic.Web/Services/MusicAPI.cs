using Newtonsoft.Json;
using ScreenMusic.Arguments;
using System.Net.Http.Json;

namespace ScreenMusic.Web.Services;

public class MusicAPI(IHttpClientFactory factory)
{
    private readonly HttpClient _httpClient = factory.CreateClient("API");

    public async Task<ICollection<OutputMusic>?> GetAll()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<OutputMusic>>("Music");
    }

    public async Task<OutputMusic?> GetByName(InputIdentifierMusic inputIdentifierMusic)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Music", inputIdentifierMusic);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            OutputMusic? outputMusic = JsonConvert.DeserializeObject<OutputMusic>(content);

            return outputMusic;
        }
        else
            return null;
    }    

    public async Task Create(InputCreateMusic inputCreateMusic)
    {
        await _httpClient.PostAsJsonAsync("Music", inputCreateMusic);
    }

    public async Task Delete(long id)
    {
        await _httpClient.DeleteAsync($"Music/{id}");
    }

    public async Task Update(long id, InputUpdateMusic inputUpdateMusic)
    {
        await _httpClient.PutAsJsonAsync($"Music/{id}", inputUpdateMusic);
    }
}