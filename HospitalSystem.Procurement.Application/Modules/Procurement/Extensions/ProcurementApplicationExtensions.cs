using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalSystem.Procurement.Application;

public static class ProcurementApplicationExtensions
{
    public static IServiceCollection AddProcurementApplication(this IServiceCollection services)
    {
        var assembly = typeof(ProcurementApplicationExtensions).Assembly;
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        return services;
    }
}
