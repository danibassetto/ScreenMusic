namespace ScreenMusic.WebMvc.DelegatingHandlers;

public class CookieHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var cookie = _httpContextAccessor.HttpContext!.Request.Cookies[".AspNetCore.Cookies"];

        if (!string.IsNullOrEmpty(cookie))
            request.Headers.Add("Cookie", $".AspNetCore.Cookies={cookie}");

        return base.SendAsync(request, cancellationToken);
    }
}