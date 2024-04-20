using System.Threading.Channels;

namespace BackgroundEventHandlerExample.MessageProcessing;

public class MessageQueue(Channel<MessageDto> _channel) : IMessageQueue
{
    public async Task EnqueueAsync(MessageDto message, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync(message, cancellationToken);
    }

    public async Task<MessageDto> DequeueAsync(CancellationToken cancellationToken)
    {
        return await _channel.Reader.ReadAsync(cancellationToken);
    }
}
