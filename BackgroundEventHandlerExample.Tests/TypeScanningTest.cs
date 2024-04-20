using BackgroundEventHandlerExample.MessageProcessing;
using BackgroundEventHandlerExample.Products;

namespace BackgroundEventHandlerExample.Tests;

public class TypeScanningTest
{
    [Fact]
    public void Test1()
    {
        var genericMessageHandlerType = typeof(IMessageHandler<>);

        var result =
            typeof(IMessageHandler<>)
                .Assembly
                .GetTypes()
                .Where(type => !type.IsAbstract && type.GetInterfaces().Any(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == genericMessageHandlerType));

        result.Should().BeEquivalentTo([typeof(CreateProductCommandHandler)]);
    }
}