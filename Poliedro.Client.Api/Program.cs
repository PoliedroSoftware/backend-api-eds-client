using Microsoft.OpenApi.Models;
using Poliedro.Billing.Api.Common.Configurations;
using Poliedro.Billing.Application;
using Poliedro.Billing.Infraestructure.Persistence.Mysql;
using Poliedro.Client.Api.Configuration;
using Poliedro.Client.Api.Extensions;
using Poliedro.Client.Api.Middleware;
using Poliedro.Client.Api.Services;
using Poliedro.Client.Application.Common.Interfaces;
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

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionConfiguration>();
});

builder.Services.AddRouting(routing => routing.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Poliedro Client API",
        Description = "API para gestión de clientes - Token validado por API Gateway"
    });

    options.EnableAnnotations();

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

app.UseMiddleware<BearerTokenMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHealthCheckEndpoints();

app.MapControllers();
app.Run();
