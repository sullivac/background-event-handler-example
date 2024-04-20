namespace BackgroundEventHandlerExample.MessageProcessing;

public interface IMessageQueue
{
    Task EnqueueAsync(MessageDto message, CancellationToken cancellationToken);

    Task<MessageDto> DequeueAsync(CancellationToken cancellationToken);
}
