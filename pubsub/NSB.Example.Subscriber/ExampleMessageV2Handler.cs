using NSB.Example.Contracs.Events;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace NSB.Example.Subscriber
{
    public class ExampleMessageV2Handler : IHandleMessages<IExampleEventV2>
    {
        public Task Handle(IExampleEventV2 message, IMessageHandlerContext context)
        {
            Console.WriteLine(message.Message + "  new" + message.NewProperty);

            return Task.CompletedTask;
        }
    }
}
