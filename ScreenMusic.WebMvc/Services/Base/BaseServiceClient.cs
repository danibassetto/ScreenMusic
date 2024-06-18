using Newtonsoft.Json;
using ScreenMusic.Arguments;
using ScreenMusic.WebMvc.Services.Base;

namespace ScreenMusic.WebMvc.Services;

public class BaseServiceClient<TInputCreate, TInputUpdate, TOutput, TIdentifier>(IHttpClientFactory factory)
{
    private readonly HttpClient _httpClient = factory.CreateClient("API");
    private readonly string NameService = typeof(TOutput).Name[6..];

    public async Task<BaseServiceClientResponse<ICollection<TOutput>>> GetAll()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<BaseResponseApi<ICollection<TOutput>>>($"api/{NameService}");
            return new BaseServiceClientResponse<ICollection<TOutput>>(true, result?.Value?.Result, null);
        }
        catch (Exception ex)
        {
            return new BaseServiceClientResponse<ICollection<TOutput>>(false, null, ex.Message);
        }
    }

    public async Task<BaseServiceClientResponse<TOutput>> GetById(long id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/{NameService}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                TOutput? outputArtist = JsonConvert.DeserializeObject<BaseResponseApi<TOutput>>(content)!.Value!.Result;
                return new BaseServiceClientResponse<TOutput>(true, outputArtist, null);
            }
            else
            {
                string content = await response.Content.ReadAsStringAsync();
                return new BaseServiceClientResponse<TOutput>(false, default, content);
            }
        }
        catch (Exception ex)
        {
            return new BaseServiceClientResponse<TOutput>(false, default, ex.Message);
        }
    }

    public async Task<BaseServiceClientResponse<TOutput>> GetByName(TIdentifier inputIdentifier)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/{NameService}/GetByIdentifier", inputIdentifier);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                TOutput? outputArtist = JsonConvert.DeserializeObject<BaseResponseApi<TOutput>>(content)!.Value!.Result;
                return new BaseServiceClientResponse<TOutput>(true, outputArtist, null);
            }
            else
            {
                string content = await response.Content.ReadAsStringAsync();
                return new BaseServiceClientResponse<TOutput>(false, default, content);
            }
        }
        catch (Exception ex)
        {
            return new BaseServiceClientResponse<TOutput>(false, default, ex.Message);
        }
    }

    public async Task<BaseServiceClientResponse<bool>> Create(TInputCreate inputCreate)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/{NameService}", inputCreate);
            response.EnsureSuccessStatusCode();
            return new BaseServiceClientResponse<bool>(true, true, null);
        }
        catch (HttpRequestException ex)
        {
            return new BaseServiceClientResponse<bool>(false, false, ex.Message);
        }
    }

    public async Task<BaseServiceClientResponse<bool>> Delete(long id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/{NameService}/{id}");
            response.EnsureSuccessStatusCode();
            return new BaseServiceClientResponse<bool>(true, true, null);
        }
        catch (HttpRequestException ex)
        {
            return new BaseServiceClientResponse<bool>(false, false, ex.Message);
        }
    }

    public async Task<BaseServiceClientResponse<bool>> Update(long id, TInputUpdate inputUpdate)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/{NameService}/{id}", inputUpdate);
            response.EnsureSuccessStatusCode();
            return new BaseServiceClientResponse<bool>(true, true, null);
        }
        catch (HttpRequestException ex)
        {
            return new BaseServiceClientResponse<bool>(false, false, ex.Message);
        }
    }
}