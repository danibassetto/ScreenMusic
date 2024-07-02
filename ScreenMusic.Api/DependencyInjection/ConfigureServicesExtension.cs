using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ScreenMusic.Domain.ApiManagement;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;
using ScreenMusic.Domain.Interfaces.Service;
using ScreenMusic.Domain.Mapping;
using ScreenMusic.Domain.Services;
using ScreenMusic.Infraestructure;
using ScreenMusic.Infraestructure.Repository;

namespace ScreenMusic.Api;

public static class ConfigureServicesExtension
{
    private static IServiceCollection ServiceCollection { get; set; } = new ServiceCollection();
    private static IConfiguration? Configuration { get; set; }

    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        ServiceCollection = serviceCollection;
        Configuration = configuration;

        AddControlers();
        AddAuthorization();
        AddOptions();
        AddTransient();
        AddSingleton();
        AddSwaggerGen();
        AddMySql();
        AddCors();
        SetApiData();

        return ServiceCollection;
    }

    private static void SetApiData()
    {
        ApiData.SetMapper(new Domain.Mapping.Mapper(new MapperConfiguration(config => { config.AddProfile(new MapperEntityOutput()); }).CreateMapper(), new MapperConfiguration(config => { config.AddProfile(new MapperInputEntity()); }).CreateMapper()));
    }

    private static void AddControlers()
    {
        ServiceCollection.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
    }

    private static void AddAuthorization()
    {
        ServiceCollection.AddAuthorization();
    }

    private static void AddOptions()
    {
        ServiceCollection.AddOptions();
    }

    private static void AddTransient()
    {
        ServiceCollection.AddTransient<IArtistService, ArtistService>();
        ServiceCollection.AddTransient<IArtistRepository, ArtistRepository>();
        ServiceCollection.AddTransient<IMusicService, MusicService>();
        ServiceCollection.AddTransient<IMusicRepository, MusicRepository>();
        ServiceCollection.AddTransient<IMusicGenreService, MusicGenreService>();
        ServiceCollection.AddTransient<IMusicGenreRepository, MusicGenreRepository>();
        ServiceCollection.AddTransient<IArtistReviewRepository, ArtistReviewRepository>();

        ServiceCollection.AddTransient<IApiDataService, ApiDataService>();
    }

    private static void AddSingleton()
    {
        var configure = new MapperConfiguration(config =>
        {
            config.AddProfile(new MapperGeneric<string, string>());
        });
        IMapper mapper = configure.CreateMapper();
        ServiceCollection.AddSingleton(mapper);
        ServiceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    private static void AddSwaggerGen()
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
        ServiceCollection.AddEndpointsApiExplorer();
    }

    private static void AddMySql()
    {
        var connectionString = Configuration!.GetConnectionString("DataBase");

        ServiceCollection.AddDbContext<ScreenMusicContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        ServiceCollection.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<ScreenMusicContext>();
    }

    private static void AddCors()
    {
        ServiceCollection.AddCors(options => options.AddPolicy("wasm", policy => policy.WithOrigins("https://localhost:7089", "https://localhost:7072").AllowAnyMethod().SetIsOriginAllowed(pol => true).AllowAnyHeader().AllowCredentials()));
    }
}