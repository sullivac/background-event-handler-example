using BackgroundEventHandlerExample.MessageProcessing;

namespace BackgroundEventHandlerExample.Products;

public class UpdateProductCommandHandler(IProductRepository _productRepository) : IMessageHandler<UpdateProductCommand>
{
    public async Task HandleAsync(UpdateProductCommand command)
    {
        await _productRepository.UpdateAsync(command.Id, new Product(command.Name));
    }
}
