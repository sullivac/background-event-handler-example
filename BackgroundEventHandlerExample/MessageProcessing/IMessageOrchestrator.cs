namespace BackgroundEventHandlerExample.MessageProcessing;

public interface IMessageOrchestrator
{
    Task ProcessAsync(MessageDto messageDto);
}
