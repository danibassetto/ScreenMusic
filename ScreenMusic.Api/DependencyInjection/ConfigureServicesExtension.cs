using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ScreenMusic.Domain.ApiManagement;
using ScreenMusic.Domain.Interfaces.Repository;
using ScreenMusic.Domain.Interfaces.Service;
using ScreenMusic.Domain.Services;
using ScreenMusic.Infraestructure;
using ScreenMusic.Infraestructure.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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
        AddCors();
        AddToken();
        AddMySql();

        return ServiceCollection;
    }

    public static void AddOptions()
    {
        ServiceCollection.AddOptions();
    }

    public static void AddTransient()
    {
        ServiceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
        ServiceCollection.AddTransient<IArtistService, ArtistService>();
        ServiceCollection.AddTransient<IArtistRepository, ArtistRepository>();
        ServiceCollection.AddTransient<IMusicService, MusicService>();
        ServiceCollection.AddTransient<IMusicRepository, MusicRepository>();
        ServiceCollection.AddTransient<IMusicGenreService, MusicGenreService>();
        ServiceCollection.AddTransient<IMusicGenreRepository, MusicGenreRepository>();
        ServiceCollection.AddTransient<IUserService, UserService>();
        ServiceCollection.AddTransient<IUserRepository, UserRepository>();

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

            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "JWT Authentication",
                Description = "Digitar somente JWT Bearer token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            });
            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        ServiceCollection.AddSwaggerGenNewtonsoftSupport();
    }

    public static void AddToken()
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        ServiceCollection.AddAuthentication((options) =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(c =>
        {
            c.RequireHttpsMetadata = false;
            c.SaveToken = true;
            c.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("63bcc5be-fa37-4290-b5a4-57a6a4e3afcb")),
                ClockSkew = TimeSpan.Zero
            };
        });
    }

    public static void AddCors()
    {
        ServiceCollection.AddCors(options => { options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });
    }

    public static void AddMySql()
    {
        var connectionString = Configuration!.GetConnectionString("DataBase");
        ServiceCollection.AddDbContext<ScreenMusicContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly("ScreenMusic.Api")));
    }
}