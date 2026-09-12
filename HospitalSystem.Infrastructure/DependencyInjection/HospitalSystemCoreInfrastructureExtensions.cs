using HospitalSystem.Application.Abstractions.Time;
using HospitalSystem.Infrastructure.Common.Time;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalSystem.Infrastructure.DependencyInjection;

public static class HospitalSystemCoreInfrastructureExtensions
{
    public static IServiceCollection AddHospitalSystemCoreInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, UtcDateTimeProvider>();
        return services;
    }
}
