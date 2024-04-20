namespace BackgroundEventHandlerExample.MessageProcessing;

public class MessageOrchestrator<TMessage>(
    MessageDeserializer<TMessage> _messageDeserializer,
    IMessageHandler<TMessage> _messageHandler) : IMessageOrchestrator where TMessage : Message
{
    public async Task ProcessAsync(MessageDto messageDto)
    {
        var message = _messageDeserializer.Deserialize(messageDto);

        await _messageHandler.HandleAsync(message);
    }
}