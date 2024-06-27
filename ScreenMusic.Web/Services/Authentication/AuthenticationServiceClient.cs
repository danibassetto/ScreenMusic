using Microsoft.AspNetCore.Components.Authorization;
using ScreenMusic.Arguments;
using System.Net.Http.Json;
using System.Security.Claims;

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
                new Claim(ClaimTypes.Name, info.Email),
                new Claim(ClaimTypes.Email, info.Email)
            ];

            var identity = new ClaimsIdentity(dados, "Cookies");
            pessoa = new ClaimsPrincipal(identity);
            authenticated = true;
        }

        return new AuthenticationState(pessoa);
    }

    public async Task<OutputAuthentication> LoginAsync(string email, string senha)
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

        return new OutputAuthentication { IsSuccess = false, ListError = ["Login/senha inválidos"] };
    }

    public async Task LogoutAsync()
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