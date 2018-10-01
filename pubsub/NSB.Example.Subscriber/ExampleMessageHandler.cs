using NSB.Example.Contracts.Events;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace NSB.Example.Subscriber
{
    [Obsolete("This handler should be removed in next version")]
    public class ExampleMessageHandler : IHandleMessages<IExampleEvent>
    {
        public Task Handle(IExampleEvent message, IMessageHandlerContext context)
        {
            if (message is IExampleEventV2)
                return Task.CompletedTask; ;

            Console.WriteLine(message.Message);

            return Task.CompletedTask;
        }
    }
}
