using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poliedro.Billing.Domain.Ports;
using Poliedro.Billing.Infraestructure.Persistence.Mysql.Adapter;
using Poliedro.Billing.Infraestructure.Persistence.Mysql.Context;

namespace Poliedro.Billing.Infraestructure.External.Plemsi
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddExternalPlemsi(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION") ?? configuration.GetConnectionString("MysqlConnection");
            services.AddDbContext<DataBaseContext>(
                options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)
            ));

            services.AddTransient<IMessageProvider, MessageProvider>();
           
           
            return services;
        }
    }
}
