using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Net.Http.Json;
using ScreenMusic.Arguments;

namespace ScreenMusic.WebMvc.Services
{
    public class AuthenticationServiceClient(IHttpClientFactory factory) : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient = factory.CreateClient("API");
        private bool _authenticated = false;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            var response = await _httpClient.GetAsync("auth/manage/info");

            if (response.IsSuccessStatusCode)
            {
                var info = await response.Content.ReadFromJsonAsync<OutputLoggedUser>();

                if (info != null && !string.IsNullOrEmpty(info.Email))
                {
                    var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name, info.Email),
                        new(ClaimTypes.Email, info.Email)
                    };

                    identity = new ClaimsIdentity(claims, "Cookies");
                    user = new ClaimsPrincipal(identity);
                    _authenticated = true;
                }
                else
                    _authenticated = false;
            }
            else
                _authenticated = false;

            return new AuthenticationState(user);
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
                _authenticated = true;
                return new OutputAuthentication { IsSuccess = true };
            }

            return new OutputAuthentication { IsSuccess = false, ListError = ["Login/senha inválidos"] };
        }

        public async Task LogoutAsync()
        {
            await _httpClient.PostAsync("auth/logout", null);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            _authenticated = false;
        }

        public bool IsAuthenticated()
        {
            return _authenticated;
        }
    }
}