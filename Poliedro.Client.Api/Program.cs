using Poliedro.Billing.Api;
using Poliedro.Billing.Api.Common.Configurations;
using Poliedro.Billing.Application;
using Poliedro.Billing.Infraestructure.External.Plemsi;
using Poliedro.Billing.Infraestructure.External.TNS;
using Poliedro.Billing.Infraestructure.Persistence.Mysql;

using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Configura el logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services
    .AddWebApi()
    .AddApplication()
    .AddExternalPlemsi(builder.Configuration)
    .AddExternalTns(builder.Configuration)
    .AddPersistence(builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionConfiguration>();
});

builder.Services.AddRouting(routing => routing.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapControllers();
app.Run();
