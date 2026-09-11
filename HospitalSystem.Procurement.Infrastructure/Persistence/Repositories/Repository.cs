using HospitalSystem.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using HospitalSystem.Domain.Reprository;

namespace HospitalSystem.Procurement.Infrastructure.Persistence.Repositories;

public abstract class Repository<TEntity, TId>(ProcurementDbContext context)
    : IRepository<TEntity, TId>
    where TEntity : AggregateRoot<TId>
    where TId : notnull
{
    protected ProcurementDbContext Context { get; } =
        context ?? throw new ArgumentNullException(nameof(context));

    protected DbSet<TEntity> DbSet => Context.Set<TEntity>();

    public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken ct = default)
        => await DbSet.FindAsync([id], ct);

    public virtual async Task AddAsync(TEntity entity, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await DbSet.AddAsync(entity, ct);
    }

    public virtual void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        DbSet.Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        DbSet.Remove(entity);
    }
}
