using BackgroundEventHandlerExample.MessageProcessing;

namespace BackgroundEventHandlerExample.Products;

public class CreateProductCommandHandler(IProductRepository _productRepository) : IMessageHandler<CreateProductCommand>
{
    public async Task HandleAsync(CreateProductCommand command)
    {
        await Console.Out.WriteLineAsync($"Creating product {command.Name}...");

        var product = new Product(command.Name);

        await _productRepository.AddAsync(product);
    }
}