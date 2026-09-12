using Microsoft.AspNetCore.Routing;

namespace HospitalSystem.Procurement.WebApi.Endpoints;
public static class ProcurementEndpoints
{
    public static IEndpointRouteBuilder MapProcurementEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapVendorsEndpoints();
        app.MapVendorContractsEndpoints();
        app.MapBudgetsEndpoints();
        app.MapPurchaseRequestsEndpoints();
        app.MapPurchaseOrdersEndpoints();
        return app;
    }
}
