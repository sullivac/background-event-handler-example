namespace BackgroundEventHandlerExample.MessageProcessing;

public class MessageOrchestratorFactory(IServiceProvider _serviceProvider)
{
    public IMessageOrchestrator Create(MessageDto messageDto)
    {
        return _serviceProvider.GetKeyedService<IMessageOrchestrator>(messageDto.Name)
            ?? throw new InvalidOperationException($"No orchestrator found for message {messageDto.Name}");
    }
}
