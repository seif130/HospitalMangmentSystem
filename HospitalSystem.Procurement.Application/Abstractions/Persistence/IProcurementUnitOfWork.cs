namespace HospitalSystem.Procurement.Application.Abstractions.Persistence;
public interface IProcurementUnitOfWork 
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);

}
