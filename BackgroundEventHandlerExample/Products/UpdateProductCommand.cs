using BackgroundEventHandlerExample.MessageProcessing;

namespace BackgroundEventHandlerExample.Products;

public record UpdateProductCommand(int Id, string Name) : Message;
