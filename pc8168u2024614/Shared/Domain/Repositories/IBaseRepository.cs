namespace pc8168u2024614.Shared.Domain.Repositories;

public interface IBaseRepository<TEntity>
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellationToken = default);
}
