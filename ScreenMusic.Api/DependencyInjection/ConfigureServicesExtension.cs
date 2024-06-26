using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ScreenMusic.Domain.ApiManagement;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;
using ScreenMusic.Domain.Interfaces.Service;
using ScreenMusic.Domain.Services;
using ScreenMusic.Infraestructure;
using ScreenMusic.Infraestructure.Repository;

namespace ScreenMusic.Api;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ServiceCollection { get; private set; } = new ServiceCollection();
    public static IConfiguration? Configuration { get; private set; }

    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        ServiceCollection = serviceCollection;
        Configuration = configuration;

        AddOptions();
        AddTransient();
        AddSingleton();
        AddSwaggerGen();
        AddMySql();

        return ServiceCollection;
    }

    public static void AddOptions()
    {
        ServiceCollection.AddOptions();
    }

    public static void AddTransient()
    {
        ServiceCollection.AddTransient<IArtistService, ArtistService>();
        ServiceCollection.AddTransient<IArtistRepository, ArtistRepository>();
        ServiceCollection.AddTransient<IMusicService, MusicService>();
        ServiceCollection.AddTransient<IMusicRepository, MusicRepository>();
        ServiceCollection.AddTransient<IMusicGenreService, MusicGenreService>();
        ServiceCollection.AddTransient<IMusicGenreRepository, MusicGenreRepository>();

        ServiceCollection.AddTransient<IApiDataService, ApiDataService>();
    }

    public static void AddSingleton()
    {
        ServiceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    public static void AddSwaggerGen()
    {
        OpenApiContact contact = new()
        {
            Name = "ScreenMusic",
            Url = new Uri("https://github.com/danibassetto")
        };

        ServiceCollection.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("pt-br", new OpenApiInfo { Title = "ScreenMusic", Version = "pt-br", Contact = contact });
        });

        ServiceCollection.AddSwaggerGenNewtonsoftSupport();
    }

    public static void AddMySql()
    {
        var connectionString = Configuration!.GetConnectionString("DataBase");
        ServiceCollection.AddDbContext<ScreenMusicContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        ServiceCollection.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<ScreenMusicContext>();
    }
}