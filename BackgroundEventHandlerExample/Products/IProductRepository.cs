namespace BackgroundEventHandlerExample.Products;

public interface IProductRepository
{
    Task AddAsync(Product product);
    
    Task<Product?> GetAsync(int id);

    Task UpdateAsync(int id, Product product);
}