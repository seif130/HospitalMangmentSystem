using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using HospitalSystem.Application.Abstractions.Events;
using HospitalSystem.Application.Common.Events;
using HospitalSystem.Application.Behaviors;

namespace HospitalSystem.Application.DependencyInjection;

public static class HospitalSystemApplicationExtensions
{
    public static IServiceCollection AddHospitalSystemApplication(
        this IServiceCollection services)
    {
        var assembly = typeof(HospitalSystemApplicationExtensions).Assembly;

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

        return services;
    }
}
