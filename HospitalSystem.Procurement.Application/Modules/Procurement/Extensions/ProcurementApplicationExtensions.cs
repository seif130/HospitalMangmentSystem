using FluentValidation;
using HospitalSystem.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using HospitalSystem.Application.Modules.Procurement.Abstractions;
using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Application.Shared.Behaviors;

namespace HospitalSystem.Application.Modules.Procurement.Extensions;

public static class ProcurementApplicationExtensions
{
    public static IServiceCollection AddProcurementApplication(this IServiceCollection services)
    {
        var assembly = typeof(ProcurementApplicationExtensions).Assembly;
        services.AddMediatR(poc =>
        {
            poc.RegisterServicesFromAssembly(assembly);
            poc.AddOpenBehavior(typeof(LoggingBehavior<,>));
            poc.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
        return services;
    }
}
