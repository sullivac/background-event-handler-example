using System.Text.Json;

namespace BackgroundEventHandlerExample.MessageProcessing;

public class MessageDeserializer<TMessage>(JsonSerializerOptions jsonSerializerOptions) where TMessage : Message
{
    public TMessage Deserialize(MessageDto messageDto)
    {
        var result = JsonSerializer.Deserialize<TMessage>(messageDto.Payload, jsonSerializerOptions);

        if (result is null)
        {
            throw new InvalidOperationException($"Failed to deserialize message of type {typeof(TMessage).Name}");
        }

        return result;
    }
}
