using Newtonsoft.Json;
using ScreenMusic.Arguments;
using System.Net.Http.Json;

namespace ScreenMusic.Web.Services;

public class BaseServiceClient<TInputCreate, TInputUpdate, TOutput, TIdentifier>(IHttpClientFactory factory)
{
    private readonly HttpClient _httpClient = factory.CreateClient("API");
    private readonly string NameService = typeof(TOutput).Name[6..];

    public async Task<ICollection<TOutput>?> GetAll()
    {
        var result = await _httpClient.GetFromJsonAsync<BaseResponseApi<ICollection<TOutput>>>($"api/{NameService}");
        return result?.Value?.Result;
    }

    public async Task<TOutput?> GetById(long id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"api/{NameService}/{id}");

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            TOutput? outputArtist = JsonConvert.DeserializeObject<BaseResponseApi<TOutput>>(content)!.Value!.Result;

            return outputArtist;
        }
        else
            return default;
    }

    public async Task<TOutput?> GetByName(TIdentifier inputIdentifier)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/{NameService}/GetByIdentifier", inputIdentifier);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            TOutput? outputArtist = JsonConvert.DeserializeObject<BaseResponseApi<TOutput>>(content)!.Value!.Result;

            return outputArtist;
        }
        else
            return default;
    }

    public async Task Create(TInputCreate inputCreate)
    {
        await _httpClient.PostAsJsonAsync($"api/{NameService}", inputCreate);
    }

    public async Task Delete(long id)
    {
        await _httpClient.DeleteAsync($"api/{NameService}/{id}");
    }

    public async Task Update(long id, TInputUpdate inputUpdate)
    {
        await _httpClient.PutAsJsonAsync($"api/{NameService}/{id}", inputUpdate);
    }
}