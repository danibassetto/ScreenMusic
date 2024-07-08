using Microsoft.AspNetCore.Authentication.Cookies;
using ScreenMusic.WebMvc.DelegatingHandlers;
using ScreenMusic.WebMvc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<CookieHandler>();
builder.Services.AddSingleton<AuthenticationServiceClient>();
builder.Services.AddTransient<ArtistServiceClient>();
builder.Services.AddTransient<MusicServiceClient>();
builder.Services.AddTransient<MusicGenreServiceClient>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Authentication/Login";
        options.LogoutPath = "/Authentication/Logout";
    });

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ScreenMusicApi:Url"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}).AddHttpMessageHandler<CookieHandler>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();