using NSB.Example.Contracs.Events;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace NSB.Example.Subscriber
{
    public class ExampleMessageHandler : IHandleMessages<IExampleEvent>
    {
        public Task Handle(IExampleEvent message, IMessageHandlerContext context)
        {
            Console.WriteLine(message.Message);

            return Task.CompletedTask;
        }
    }
}
