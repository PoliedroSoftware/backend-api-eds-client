using Poliedro.Client.Api.Extensions;
using Poliedro.Billing.Api.Common.Configurations;
using Poliedro.Billing.Application;
using Poliedro.Billing.Infraestructure.Persistence.Mysql;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Configura el logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration);

// Add Custom Health Checks
builder.Services.AddCustomHealthChecks();

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

// Configure Health Check endpoints
app.UseHealthCheckEndpoints();

app.MapControllers();
app.Run();
