using AdminApp.Core.Context;
using AdminApp.Infrastructure.Repositories;
using AdminApp.Services.Services;
using AdminApp.Utils;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8000";
builder.WebHost.UseUrls($"http://*:{port}");

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

#region Registro dinámico de repositorios (Dynamic Repositories Injection)
var generalRepositories = typeof(_BaseRepository<>).Assembly.GetTypes()
    .Where(type => !type.Name.StartsWith("_") && type.Name.EndsWith("Repository"))
    .ToList();

var repositoryInterfaces = generalRepositories.Where(type => type.IsInterface);
var repositoryImplementations = generalRepositories.Where(type => type.IsClass);

foreach (var implementation in repositoryImplementations)
{
    var interfaceName = $"I{implementation.Name}";
    var repositoryInterface = repositoryInterfaces.FirstOrDefault(i => i.Name == interfaceName);
    if (repositoryInterface != null)
    {
        builder.Services.AddScoped(repositoryInterface, implementation);
    }
}
#endregion

#region Registro dinámico de servicios (Dynamic Service Injection)
var generalServices = typeof(_BaseService<>).Assembly.GetTypes()
    .Where(type => !type.Name.StartsWith("_") && type.Name.EndsWith("Service"))
    .ToList();

var serviceInterfaces = generalServices.Where(type => type.IsInterface);
var serviceImplementations = generalServices.Where(type => type.IsClass);

foreach (var implementation in serviceImplementations)
{
    var interfaceName = $"I{implementation.Name}";
    var serviceInterface = serviceInterfaces.FirstOrDefault(i => i.Name == interfaceName);
    if (serviceInterface != null)
    {
        builder.Services.AddScoped(serviceInterface, implementation);
    }
}
#endregion

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.UseHealthChecks("/health");

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
