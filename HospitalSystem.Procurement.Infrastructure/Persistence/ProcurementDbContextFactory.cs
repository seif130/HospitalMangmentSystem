using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HospitalSystem.Procurement.Infrastructure.Persistence;

public sealed class ProcurementDbContextFactory
    : IDesignTimeDbContextFactory<ProcurementDbContext>
{
    public ProcurementDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__ProcurementConnection")
            ?? "Server=.;Database=HospitalSystem_Procurement;Trusted_Connection=True;TrustServerCertificate=True";

        var options = new DbContextOptionsBuilder<ProcurementDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new ProcurementDbContext(options);
    }
}
