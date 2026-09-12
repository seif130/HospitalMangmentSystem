using HospitalSystem.Domain.Primitives;

namespace HospitalSystem.Application.Abstractions.Persistence;

public interface IRepository<TAggregate, TId>
    where TAggregate : AggregateRoot<TId>
    where TId : notnull
{
    Task<TAggregate?> GetByIdAsync(
        TId id,
        CancellationToken ct = default);

    Task AddAsync(
        TAggregate aggregate,
        CancellationToken ct = default);

    void Update(TAggregate aggregate);

    void Remove(TAggregate aggregate);
}
