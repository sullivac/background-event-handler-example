using System.Reflection;
using System.Threading.Channels;

namespace BackgroundEventHandlerExample.MessageProcessing;

public static class MessageHandlerRegistration
{
    private readonly static Type GenericMessageHandlerType = typeof(IMessageHandler<>);

    private readonly static Type GenericMessageOrchestratorType = typeof(MessageOrchestrator<>);

    public static IServiceCollection AddMessageHandlers(this IServiceCollection services)
    {
        services.AddHostedService<MessageBackgroundService>();

        services.AddSingleton<IMessageQueue, MessageQueue>(_ => new MessageQueue(Channel.CreateUnbounded<MessageDto>()));
        services.AddSingleton(typeof(MessageDeserializer<>));

        services.AddScoped<MessageOrchestratorFactory>();

        var messageHandlerRegistrations =
            Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(IsMessageHandler)
                .SelectMany(
                    messageHandlerType =>
                        messageHandlerType
                            .GetInterfaces()
                            .Where(IsMessageHandlerInterface)
                            .Select(
                                messageHandlerInterfaceType =>
                                    new
                                    {
                                        MessageHandlerType = messageHandlerType,
                                        MessageType = messageHandlerInterfaceType.GetGenericArguments().Single(),
                                        MessageHandlerInterfaceType = messageHandlerInterfaceType
                                    }));

        foreach (var registration in messageHandlerRegistrations)
        {
            services.AddScoped(registration.MessageHandlerInterfaceType, registration.MessageHandlerType);

            var closedMessageOrchestratorType = GenericMessageOrchestratorType.MakeGenericType(registration.MessageType);

            services.AddKeyedScoped(typeof(IMessageOrchestrator), registration.MessageType.Name, closedMessageOrchestratorType);
        }

        return services;
    }

    private static bool IsMessageHandler(Type type)
    {
        return type.GetInterfaces().Any(IsMessageHandlerInterface);
    }

    private static bool IsMessageHandlerInterface(Type interfaceType)
    {
        return interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == GenericMessageHandlerType;
    }
}
