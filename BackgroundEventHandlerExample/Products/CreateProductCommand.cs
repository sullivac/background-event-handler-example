using BackgroundEventHandlerExample.MessageProcessing;

namespace BackgroundEventHandlerExample.Products;

public record CreateProductCommand(string Name) : Message;
