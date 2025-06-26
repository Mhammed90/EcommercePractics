namespace ECommerceApp.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _context.Set<T>().FindAsync(new object[] { id }, ct);


    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
        => await _context.Set<T>().AsNoTracking().ToListAsync(ct);


    public async Task<IEnumerable<T>> FindAllAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken ct = default)
        => await _context.Set<T>().AsNoTracking()
            .Where(predicate)
            .ToListAsync(ct);


    public async Task<T?> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken ct = default) => await _context.Set<T>().FirstOrDefaultAsync(predicate, ct);


    public async Task AddAsync(T entity, CancellationToken ct = default)
        => await _context.Set<T>().AddAsync(entity, ct);


    public void Update(T entity)
        => _context.Set<T>().Update(entity);


    public void Delete(T entity)
        => _context.Set<T>().Remove(entity);
}