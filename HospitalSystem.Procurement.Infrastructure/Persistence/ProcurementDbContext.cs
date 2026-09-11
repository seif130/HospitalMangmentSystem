using HospitalSystem.Domain.Modules.Procurement.Budgets;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts;
using HospitalSystem.Domain.Modules.Procurement.Vendors;
using HospitalSystem.Procurement.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Procurement.Infrastructure.Persistence;

public sealed class ProcurementDbContext(
    DbContextOptions<ProcurementDbContext> options) : DbContext(options)
{
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<VendorContract> VendorContracts => Set<VendorContract>();
    public DbSet<Budget> Budgets => Set<Budget>();
    public DbSet<PurchaseRequest> PurchaseRequests => Set<PurchaseRequest>();
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProcurementDbContext).Assembly);
    }
}
