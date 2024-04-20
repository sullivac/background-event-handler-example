namespace BackgroundEventHandlerExample.MessageProcessing;

public class MessageBackgroundService(IMessageQueue _messageQueue, IServiceProvider _serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = await _messageQueue.DequeueAsync(stoppingToken);
            if (message is not null)
            {
                using var scope = _serviceProvider.CreateScope();

                var messageOrchestrator = scope.ServiceProvider.GetKeyedService<IMessageOrchestrator>(message.Name);
                if (messageOrchestrator is not null)
                {
                    await messageOrchestrator.ProcessAsync(message);
                }
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
    }
}
