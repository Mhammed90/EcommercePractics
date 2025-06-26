namespace ECommerceApp.Repositories;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Product> ProductRepository { get; }
    IGenericRepository<Category> CategoryRepository { get; }

    Task<int> CommitAsync(CancellationToken ct = default);
}