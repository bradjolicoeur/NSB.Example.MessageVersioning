using NSB.Example.Contracts.Events;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace NSB.Example.Subscriber
{
    public class ExampleMessageHandlerV2 : IHandleMessages<IExampleEventV2>
    {
        public Task Handle(IExampleEventV2 message, IMessageHandlerContext context)
        {
            Console.WriteLine(message.Message + " " + message.NewProperty);

            return Task.CompletedTask;
        }
    }
}
