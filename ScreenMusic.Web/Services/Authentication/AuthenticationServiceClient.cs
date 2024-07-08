using Microsoft.AspNetCore.Components.Authorization;
using ScreenMusic.Arguments;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace ScreenMusic.Web.Services;

public class AuthenticationServiceClient(IHttpClientFactory factory) : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient = factory.CreateClient("API");
    private bool authenticated = false;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        authenticated = false;
        var pessoa = new ClaimsPrincipal();
        var response = await _httpClient.GetAsync("auth/manage/info");

        if (response.IsSuccessStatusCode)
        {
            var info = await response.Content.ReadFromJsonAsync<OutputLoggedUser>();
            Claim[] dados =
            [
                new Claim(ClaimTypes.Name, info?.Email!),
                new Claim(ClaimTypes.Email, info?.Email!)
            ];

            var identity = new ClaimsIdentity(dados, "Cookies");
            pessoa = new ClaimsPrincipal(identity);
            authenticated = true;
        }

        return new AuthenticationState(pessoa);
    }

    public async Task<OutputAuthentication> Login(string email, string senha)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login?useCookies=true", new
        {
            email,
            password = senha
        });

        if (response.IsSuccessStatusCode)
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return new OutputAuthentication { IsSuccess = true };
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            var apiErrorResponse = JsonSerializer.Deserialize<OutputAuthenticationError>(errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new OutputAuthentication { IsSuccess = false, Error = string.Join("; ", apiErrorResponse?.Errors?.SelectMany(e => e.Value)!) };
        }
    }

    public async Task<OutputAuthentication> Register(string email, string senha)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/register", new
        {
            email,
            password = senha
        });

        if (response.IsSuccessStatusCode)
            return new OutputAuthentication { IsSuccess = true };
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            var apiErrorResponse = JsonSerializer.Deserialize<OutputAuthenticationError>(errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new OutputAuthentication { IsSuccess = false, Error = string.Join("; ", apiErrorResponse?.Errors?.SelectMany(e => e.Value)!) };
        }
    }

    public async Task Logout()
    {
        await _httpClient.PostAsync("auth/logout", null);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task<bool> VerifyAuthenticated()
    {
        await GetAuthenticationStateAsync();
        return authenticated;
    }
}