using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using HospitalSystem.Procurement.Infrastructure.Persistence;
using HospitalSystem.Procurement.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalSystem.Procurement.Infrastructure.DependencyInjection;

public static class ProcurementInfrastructureExtensions
{
    public static IServiceCollection AddProcurementInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ProcurementDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("Procurement")
                ?? throw new InvalidOperationException(
                    "Connection string 'Procurement' was not found.")));

        services.AddScoped<IVendorRepository, VendorRepository>();
        services.AddScoped<IVendorContractRepository, VendorContractRepository>();
        services.AddScoped<IBudgetRepository, BudgetRepository>();
        services.AddScoped<IPurchaseRequestRepository, PurchaseRequestRepository>();
        services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();

        services.AddScoped<IProcurementUnitOfWork, ProcurementUnitOfWork>();

        return services;
    }
}
