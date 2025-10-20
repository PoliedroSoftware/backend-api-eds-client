using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Poliedro.Billing.Application.Common.Behaviors;
using Poliedro.Client.Application.Client.Mappers;
using System.Reflection;

namespace Poliedro.Billing.Application;

public static class DependencyInjectionService
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        #region Mappers
        var mapper = new MapperConfiguration(config =>
        {
            config.AddProfile(new ClientProfile());
        });
        services.AddSingleton(mapper.CreateMapper());
        #endregion

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehaviour<,>)
        );

        return services;
    }
}