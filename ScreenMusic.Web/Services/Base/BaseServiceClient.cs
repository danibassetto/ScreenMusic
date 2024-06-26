﻿using Newtonsoft.Json;
using ScreenMusic.Arguments;
using System.Net.Http.Json;

namespace ScreenMusic.Web.Services;

public class BaseServiceClient<TInputCreate, TInputUpdate, TOutput, TIdentifier>(IHttpClientFactory factory)
{
    protected readonly HttpClient _httpClient = factory.CreateClient("API");
    protected readonly string NameService = typeof(TOutput).Name[6..];

    public async Task<ICollection<TOutput>?> GetAll()
    {
        var result = await _httpClient.GetFromJsonAsync<BaseResponseApi<ICollection<TOutput>>>($"api/{NameService}");
        return result?.Result;
    }

    public async Task<TOutput?> GetById(long id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"api/{NameService}/{id}");

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            TOutput? outputArtist = JsonConvert.DeserializeObject<BaseResponseApi<TOutput>>(content)!.Result;

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

            TOutput? outputArtist = JsonConvert.DeserializeObject<BaseResponseApi<TOutput>>(content)!.Result;

            return outputArtist;
        }
        else
            return default;
    }

    public async Task<bool> Create(TInputCreate inputCreate)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/{NameService}", inputCreate);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (HttpRequestException)
        {
            return false;
        }
    }

    public async Task<bool> Delete(long id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/{NameService}/{id}");
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (HttpRequestException)
        {
            return false;
        }
    }

    public async Task<bool> Update(long id, TInputUpdate inputUpdate)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/{NameService}/{id}", inputUpdate);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (HttpRequestException)
        {
            return false;
        }
    }
}