using Poliedro.Billing.Api.Common.Configurations;
using Poliedro.Billing.Application;
using Poliedro.Billing.Infraestructure.Persistence.Mysql;
using Poliedro.Client.Api.Configuration;
using Poliedro.Client.Api.Endpoints;
using Poliedro.Client.Api.Extensions;
using Poliedro.Client.Api.Middleware;
using Poliedro.Client.Api.Services;
using Poliedro.Client.Application.Common.Interfaces;
using Scalar.AspNetCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.Configure<RedisSettings>(config.GetSection("Redis"));

var redisConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION")
    ?? config.GetSection("Redis:ConnectionString").Value;

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString!));
builder.Services.AddSingleton<ICacheService, RedisCacheService>();

builder.Services
  .AddApplication()
    .AddPersistence(builder.Configuration);

builder.Services.AddCustomHealthChecks();

// Add global exception filter configuration
builder.Services.AddSingleton<GlobalExceptionConfiguration>();

builder.Services.AddRouting(routing => routing.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();

// Add OpenAPI
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliedroBilling", policy =>
    {
        var allowedOrigins = config.GetSection("AllowedOrigins").Get<List<string>>();
        policy.WithOrigins(allowedOrigins.ToArray())
     .AllowAnyMethod()
       .AllowAnyHeader()
     .AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors("PoliedroBilling");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Map OpenAPI endpoint
app.MapOpenApi();

// Add Scalar UI - Configured to be the default documentation page
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Poliedro Client API")
        .WithTheme(ScalarTheme.Purple)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithPreferredScheme("Bearer")
        .WithApiKeyAuthentication(x => x.Token = "");
});

// Health checks
app.UseHealthCheckEndpoints();

// Custom authentication middleware
app.UseMiddleware<BearerTokenMiddleware>();

// Map Minimal API endpoints
app.MapClientEndpoints();
app.MapDocumentTypeEndpoints();
app.MapHealthApiEndpoints();

app.Run();
