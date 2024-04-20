namespace BackgroundEventHandlerExample.MessageProcessing;

public interface IMessageHandler<TMessage> where TMessage : Message
{
    Task HandleAsync(TMessage message);
}