using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BackgroundEventHandlerExample.MessageProcessing;

public class MessageDto
{
    [Required]
    public string? Name { get; init; }

    [Required]
    public JsonElement Payload { get; init; }
}
