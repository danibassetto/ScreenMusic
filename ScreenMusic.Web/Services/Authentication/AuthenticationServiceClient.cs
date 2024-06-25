using Microsoft.AspNetCore.Components.Authorization;
using ScreenMusic.Arguments;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ScreenMusic.Web.Services;

public class AuthenticationServiceClient(IHttpClientFactory factory) : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient = factory.CreateClient("API");

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var person = new ClaimsPrincipal();

        var response = await _httpClient.GetAsync("auth/manage/info");

        if(response.IsSuccessStatusCode)
        {
            var information = await response.Content.ReadFromJsonAsync<OutputLoggedUser>();
            Claim[] data = [
                new Claim(ClaimTypes.Name, information.Email),
                new Claim(ClaimTypes.Email, information.Email)
            ];

            var identity = new ClaimsIdentity(data, "Cookies");
            person = new ClaimsPrincipal(identity);
        }

        return new AuthenticationState(person);
    }

    public async Task<OutputAuthentication> LoginAsync(string email, string senha)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login", new
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
}