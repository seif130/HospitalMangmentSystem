using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.WebApi.Modules.Procurement.Endpoints
{
    public static class ProcurementEndpointExtensions
    {
        public static IEndpointRouteBuilder MapProcurementEndpoints(
            this IEndpointRouteBuilder app)
        {
            var group = app
                .MapGroup("/api/procurement")
                .WithTags("Procurement");

            group.MapVendorEndpoints();
            group.MapVendorContractEndpoints();
            group.MapBudgetEndpoints();
            group.MapPurchaseRequestEndpoints();
            group.MapPurchaseOrderEndpoints();

            return app;
        }
    }
}
