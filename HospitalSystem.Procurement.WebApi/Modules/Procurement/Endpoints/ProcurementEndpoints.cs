using HospitalSystem.Application.Common.Models;
using HospitalSystem.Application.Modules.Procurement.Budgets.Commands;
using HospitalSystem.Application.Modules.Procurement.Budgets.Queries;
using HospitalSystem.Application.Modules.Procurement.DTOs;
using HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands;
using HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Queries;
using HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands;
using HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Queries;
using HospitalSystem.Application.Modules.Procurement.VendorContracts.Commands;
using HospitalSystem.Application.Modules.Procurement.VendorContracts.Queries;
using HospitalSystem.Application.Modules.Procurement.Vendors.Commands;
using HospitalSystem.Application.Modules.Procurement.Vendors.Queries;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Enums;
using MediatR;
using HospitalSystem.WebApi.Infrastructure;

namespace HospitalSystem.WebApi.Modules.Procurement.Endpoints;

public static class ProcurementEndpoints
{
    public static IEndpointRouteBuilder MapProcurementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/procurement").WithTags("Procurement");
        MapVendors(group.MapGroup("/vendors").WithTags("Vendors"));
        MapContracts(group.MapGroup("/vendor-contracts").WithTags("Vendor Contracts"));
        MapBudgets(group.MapGroup("/budgets").WithTags("Budgets"));
        MapPurchaseRequests(group.MapGroup("/purchase-requests").WithTags("Purchase Requests"));
        MapPurchaseOrders(group.MapGroup("/purchase-orders").WithTags("Purchase Orders"));
        return app;
    }

    private static void MapVendors(RouteGroupBuilder g)
    {
        g.MapPost("", async (CreateVendorRequest body, ISender s, CancellationToken ct) =>
            await EndpointHelper.SendAsync(new CreateVendorCommand(body.Name, body.ContactEmail, body.ContactPhone), s, ct,
                id => Results.Created($"/api/procurement/vendors/{id.Value}", id.Value)))
            .WithName("CreateVendor");

        g.MapGet("/{id:guid}", async (Guid id, ISender s, CancellationToken ct) =>
            await EndpointHelper.SendAsync<GetVendorByIdQuery, VendorDto>(new(new VendorId(id)), s, ct));

        g.MapGet("", async (int pageNumber, int pageSize, ISender s, CancellationToken ct) =>
            await EndpointHelper.SendAsync<GetVendorsQuery, PaginatedList<VendorDto>>(new(pageNumber == 0 ? 1 : pageNumber, pageSize == 0 ? 20 : pageSize), s, ct));

        g.MapPut("/{id:guid}", async (Guid id, RenameVendorRequest body, ISender s, CancellationToken ct) =>
            await EndpointHelper.SendAsync(new RenameVendorCommand(new VendorId(id), body.Name), s, ct));

        g.MapPut("/{id:guid}/contact", async (Guid id, UpdateContactRequest body, ISender s, CancellationToken ct) =>
            await EndpointHelper.SendAsync(new UpdateVendorContactCommand(new VendorId(id), body.ContactEmail, body.ContactPhone), s, ct));

        g.MapPost("/{id:guid}/activate", async (Guid id, ISender s, CancellationToken ct) =>
            await EndpointHelper.SendAsync(new ActivateVendorCommand(new VendorId(id)), s, ct));
        g.MapPost("/{id:guid}/deactivate", async (Guid id, ISender s, CancellationToken ct) =>
            await EndpointHelper.SendAsync(new DeactivateVendorCommand(new VendorId(id)), s, ct));
    }

    private static void MapContracts(RouteGroupBuilder g)
    {
        g.MapPost("", async (CreateContractRequest b, ISender s, CancellationToken ct) =>
            await EndpointHelper.SendAsync(new CreateVendorContractCommand(new VendorId(b.VendorId), b.Category, b.StartUtc, b.EndUtc, b.Amount, b.Currency), s, ct,
                id => Results.Created($"/api/procurement/vendor-contracts/{id.Value}", id.Value)));
        g.MapGet("/{id:guid}", async (Guid id, ISender s, CancellationToken ct) =>
            await EndpointHelper.SendAsync<GetVendorContractByIdQuery, VendorContractDto>(new(new VendorContractId(id)), s, ct));
        g.MapGet("/vendor/{vendorId:guid}", async (Guid vendorId, int pageNumber, int pageSize, ISender s, CancellationToken ct) =>
            await EndpointHelper.SendAsync<GetVendorContractsByVendorQuery, PaginatedList<VendorContractDto>>(new(new VendorId(vendorId), pageNumber == 0 ? 1 : pageNumber, pageSize == 0 ? 20 : pageSize), s, ct));
        g.MapPost("/{id:guid}/activate", async (Guid id, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new ActivateVendorContractCommand(new VendorContractId(id)), s, ct));
        g.MapPost("/{id:guid}/expire", async (Guid id, DateTime? asOfUtc, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new ExpireVendorContractCommand(new VendorContractId(id), asOfUtc ?? DateTime.UtcNow), s, ct));
        g.MapPost("/{id:guid}/terminate", async (Guid id, TerminateRequest b, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new TerminateVendorContractCommand(new VendorContractId(id), b.Reason), s, ct));
    }

    private static void MapBudgets(RouteGroupBuilder g)
    {
        g.MapPost("", async (AllocateBudgetRequest b, ISender s, CancellationToken ct) =>
            await EndpointHelper.SendAsync(new AllocateBudgetCommand(new DepartmentId(b.DepartmentId), b.FiscalStart, b.FiscalEnd, b.Amount, b.Currency), s, ct,
                id => Results.Created($"/api/procurement/budgets/{id.Value}", id.Value)));
        g.MapGet("/{id:guid}", async (Guid id, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync<GetBudgetByIdQuery, BudgetDto>(new(new BudgetId(id)), s, ct));
        g.MapGet("/department/{departmentId:guid}", async (Guid departmentId, int pageNumber, int pageSize, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync<GetBudgetsByDepartmentQuery, PaginatedList<BudgetDto>>(new(new DepartmentId(departmentId), pageNumber == 0 ? 1 : pageNumber, pageSize == 0 ? 20 : pageSize), s, ct));
        g.MapPost("/{id:guid}/expenses", async (Guid id, RecordExpenseRequest b, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new RecordBudgetExpenseCommand(new BudgetId(id), b.Description, b.Amount, b.Currency, b.IncurredOnUtc), s, ct));
    }

    private static void MapPurchaseRequests(RouteGroupBuilder g)
    {
        g.MapPost("", async (CreatePurchaseRequestRequest b, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new CreatePurchaseRequestCommand(new DepartmentId(b.DepartmentId), b.Reason), s, ct,
            id => Results.Created($"/api/procurement/purchase-requests/{id.Value}", id.Value)));
        g.MapGet("/{id:guid}", async (Guid id, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync<GetPurchaseRequestByIdQuery, PurchaseRequestDto>(new(new PurchaseRequestId(id)), s, ct));
        g.MapGet("/department/{departmentId:guid}", async (Guid departmentId, int pageNumber, int pageSize, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync<GetPurchaseRequestsByDepartmentQuery, PaginatedList<PurchaseRequestDto>>(new(new DepartmentId(departmentId), pageNumber == 0 ? 1 : pageNumber, pageSize == 0 ? 20 : pageSize), s, ct));
        g.MapPost("/{id:guid}/lines", async (Guid id, RequestLine b, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new AddPurchaseRequestLineCommand(new PurchaseRequestId(id), b.ItemName, b.Quantity, b.UnitPrice, b.Currency), s, ct));
        g.MapPost("/{id:guid}/submit", async (Guid id, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new SubmitPurchaseRequestCommand(new PurchaseRequestId(id)), s, ct));
        g.MapPost("/{id:guid}/approve", async (Guid id, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new ApprovePurchaseRequestCommand(new PurchaseRequestId(id)), s, ct));
        g.MapPost("/{id:guid}/reject", async (Guid id, RejectRequest b, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new RejectPurchaseRequestCommand(new PurchaseRequestId(id), b.Reason), s, ct));
        g.MapPost("/{id:guid}/cancel", async (Guid id, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new CancelPurchaseRequestCommand(new PurchaseRequestId(id)), s, ct));
    }

    private static void MapPurchaseOrders(RouteGroupBuilder g)
    {
        g.MapPost("", async (CreatePurchaseOrderRequest b, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new CreatePurchaseOrderCommand(new VendorId(b.VendorId), b.Currency, b.PurchaseRequestId.HasValue ? new PurchaseRequestId(b.PurchaseRequestId.Value) : null), s, ct,
            id => Results.Created($"/api/procurement/purchase-orders/{id.Value}", id.Value)));
        g.MapGet("/{id:guid}", async (Guid id, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync<GetPurchaseOrderByIdQuery, PurchaseOrderDto>(new(new PurchaseOrderId(id)), s, ct));
        g.MapGet("/vendor/{vendorId:guid}", async (Guid vendorId, int pageNumber, int pageSize, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync<GetPurchaseOrdersByVendorQuery, PaginatedList<PurchaseOrderDto>>(new(new VendorId(vendorId), pageNumber == 0 ? 1 : pageNumber, pageSize == 0 ? 20 : pageSize), s, ct));
        g.MapGet("/purchase-request/{requestId:guid}", async (Guid requestId, int pageNumber, int pageSize, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync<GetPurchaseOrdersByPurchaseRequestQuery, PaginatedList<PurchaseOrderDto>>(new(new PurchaseRequestId(requestId), pageNumber == 0 ? 1 : pageNumber, pageSize == 0 ? 20 : pageSize), s, ct));
        g.MapPost("/{id:guid}/lines", async (Guid id, OrderLine b, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new AddPurchaseOrderLineCommand(new PurchaseOrderId(id), b.ItemName, b.Quantity, b.UnitPrice, b.Currency), s, ct));
        g.MapPost("/{id:guid}/submit", async (Guid id, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new SubmitPurchaseOrderCommand(new PurchaseOrderId(id)), s, ct));
        g.MapPost("/{id:guid}/approve", async (Guid id, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new ApprovePurchaseOrderCommand(new PurchaseOrderId(id)), s, ct));
        g.MapPost("/{id:guid}/cancel", async (Guid id, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new CancelPurchaseOrderCommand(new PurchaseOrderId(id)), s, ct));
        g.MapPost("/{id:guid}/complete", async (Guid id, ISender s, CancellationToken ct) => await EndpointHelper.SendAsync(new CompletePurchaseOrderCommand(new PurchaseOrderId(id)), s, ct));
    }

    public sealed record CreateVendorRequest(string Name, string? ContactEmail, string? ContactPhone);
    public sealed record RenameVendorRequest(string Name);
    public sealed record UpdateContactRequest(string? ContactEmail, string? ContactPhone);
    public sealed record CreateContractRequest(Guid VendorId, VendorServiceCategory Category, DateTime StartUtc, DateTime EndUtc, decimal Amount, string Currency = "USD");
    public sealed record TerminateRequest(string Reason);
    public sealed record AllocateBudgetRequest(Guid DepartmentId, DateTime FiscalStart, DateTime FiscalEnd, decimal Amount, string Currency = "USD");
    public sealed record RecordExpenseRequest(string Description, decimal Amount, string Currency, DateTime IncurredOnUtc);
    public sealed record CreatePurchaseRequestRequest(Guid DepartmentId, string Reason);
    public sealed record RequestLine(string ItemName, int Quantity, decimal UnitPrice, string Currency = "USD");
    public sealed record RejectRequest(string Reason);
    public sealed record CreatePurchaseOrderRequest(Guid VendorId, string Currency = "USD", Guid? PurchaseRequestId = null);
    public sealed record OrderLine(string ItemName, int Quantity, decimal UnitPrice, string Currency = "USD");
}
