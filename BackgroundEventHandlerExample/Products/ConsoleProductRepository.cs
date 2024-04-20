namespace BackgroundEventHandlerExample.Products;

public class ConsoleProductRepository : IProductRepository
{
    private static int _id = 0;

    private static readonly Dictionary<int, Product> _products = [];

    public async Task AddAsync(Product product)
    {
        Interlocked.Increment(ref _id);

        _products[_id] = product;

        await Console.Out.WriteLineAsync($"Product {_id} {product.Name} added to the repository.");
    }

    public async Task<Product?> GetAsync(int id)
    {
        if (_products.ContainsKey(id))
        {
            await Console.Out.WriteLineAsync($"Product {id} retrieved from the repository.");

            return _products[id];
        }
        else
        {
            await Console.Out.WriteLineAsync($"Product with id {id} not found in the repository.");

            return null;
        }
    }

    public async Task UpdateAsync(int id, Product product)
    {
        if (_products.ContainsKey(id))
        {
            _products[id] = product;

            await Console.Out.WriteLineAsync($"Product {product.Name} updated in the repository.");
        }
        else
        {
            await Console.Out.WriteLineAsync($"Product with id {id} not found in the repository.");
        }
    }
}
