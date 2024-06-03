using AutoMapper;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ScreenMusic.Api;
using ScreenMusic.Domain.ApiManagement;
using ScreenMusic.Domain.Mapping;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(c => { c.AddPolicy("CorsPolicy", options => { options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }); });

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    options.SerializerSettings.Formatting = Formatting.Indented;
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.ConfigureDependencyInjection(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScreenMusic", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var configure = new MapperConfiguration(config =>
{
    config.AddProfile(new MapperGeneric<string, string>());
});
IMapper mapper = configure.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddCors(
    options => options.AddPolicy(
        "wasm",
        policy => policy.WithOrigins("https://localhost:7089", "https://localhost:7072")
            .AllowAnyMethod()
            .SetIsOriginAllowed(pol => true)
            .AllowAnyHeader()
            .AllowCredentials()));
#region ApiData
ApiData.SetMapper(new ScreenMusic.Domain.Mapping.Mapper(new MapperConfiguration(config => { config.AddProfile(new MapperEntityOutput()); }).CreateMapper(), new MapperConfiguration(config => { config.AddProfile(new MapperInputEntity()); }).CreateMapper()));
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/pt-br/swagger.json", "ScreenMusic");
    });
}

app.UseHttpsRedirection();

//app.UseAuthentication();

app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.UseCors("wasm");

app.MapControllers();

app.Run();