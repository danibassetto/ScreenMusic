using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ScreenMusic.Arguments;
using System.Security.Claims;
using System.Text.Json;

namespace ScreenMusic.WebMvc.Services;

public class AuthenticationServiceClient(IHttpClientFactory factory, IHttpContextAccessor httpContextAccessor)
{
    private readonly HttpClient _httpClient = factory.CreateClient("API");
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

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
            var outputAuthenticationError = JsonSerializer.Deserialize<OutputAuthenticationError>(errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new OutputAuthentication { IsSuccess = false, Error = outputAuthenticationError?.Errors?.ToList().Count > 0 ? string.Join("; ", outputAuthenticationError?.Errors?.ToList()!) : "Email/senha inválido"};
        }
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
            var claims = new List<Claim>
            {
                 new(ClaimTypes.Name, email),
                 new(ClaimTypes.Email, email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await _httpContextAccessor.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties { IsPersistent = true });

            return new OutputAuthentication { IsSuccess = true };
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            var outputAuthenticationError = JsonSerializer.Deserialize<OutputAuthenticationError>(errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new OutputAuthentication { IsSuccess = false, Error = outputAuthenticationError?.Errors?.ToList().Count > 0 ? string.Join("; ", outputAuthenticationError?.Errors?.ToList()!) : "Login/senha inválido." };
        }
    }

    public async Task Logout()
    {
        await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public bool IsAuthenticated()
    {
        return _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated;
    }
}