using ECommerceApp.Data;

namespace ECommerceApp.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    private IUnitOfWork _unitOfWorkImplementation;


    public IGenericRepository<Product> ProductRepository { get; private set; }
    public IGenericRepository<Category> CategoryRepository { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        ProductRepository = new GenericRepository<Product>(_context);
        CategoryRepository = new GenericRepository<Category>(_context);
    }


    public async Task<int> CommitAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    
}