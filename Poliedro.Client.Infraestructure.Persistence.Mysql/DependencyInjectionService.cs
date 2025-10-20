using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poliedro.Billing.Domain.Ports;
using Poliedro.Billing.Infraestructure.Persistence.Mysql.Adapter;
using Poliedro.Client.Domain.ClientPos.DomainServices;
using Poliedro.Client.Infraestructure.Persistence.Mysql.Client.DomainServices;
using Poliedro.Client.Infraestructure.Persistence.Mysql.Context;

namespace Poliedro.Billing.Infraestructure.Persistence.Mysql;

public static class DependencyInjectionService
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION") ?? configuration.GetConnectionString("MysqlConnection");
        services.AddDbContext<ClientDbContext>(
            options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)
        ));
        services.AddTransient<IMessageProvider, MessageProvider>();
        services.AddScoped<IClientDomainService, ClientDomainService>();
        services.AddScoped<IDocumentTypeDomainService, DocumentTypeDomainService>();
        
        return services;
    }
}
