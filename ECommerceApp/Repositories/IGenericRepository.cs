namespace ECommerceApp.Repositories;

using System.Threading;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);

    Task<IEnumerable<T>> FindAllAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken ct = default);

    Task<T?> FindAsync(Expression<Func<T, bool>> predicate,
        CancellationToken ct = default);

    Task AddAsync(T entity, CancellationToken ct = default);
    void Update(T entity);
    void Delete(T entity);
}