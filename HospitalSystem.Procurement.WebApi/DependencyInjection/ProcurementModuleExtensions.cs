using HospitalSystem.Procurement.Application;
using HospitalSystem.Procurement.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalSystem.Procurement.WebApi.DependencyInjection;

public static class ProcurementModuleExtensions
{
    public static IServiceCollection AddProcurementModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddProcurementApplication();
        services.AddProcurementInfrastructure(configuration);
        return services;
    }
}
