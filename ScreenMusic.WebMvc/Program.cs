using ScreenMusic.WebMvc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ScreenMusicApi:Url"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddTransient<ArtistServiceClient>();
builder.Services.AddTransient<MusicServiceClient>();
builder.Services.AddTransient<MusicGenreServiceClient>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
