using NSB.Example.Contracts.Events;
using NServiceBus;
using System;
using System.Threading;

namespace NSB.Example.Publisher
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Publisher";

            EndpointConfiguration endpointConfiguration = ConfigureNSB();

            var endpointInstance = Endpoint.Start(endpointConfiguration)
              .GetAwaiter().GetResult();

            while (true)
            {
                var id = Guid.NewGuid().ToString();
                endpointInstance.Publish<IExampleEvent>(
                        messageConstructor: message =>
                        {
                            message.Message = "message: " + id;
                        })
                 .GetAwaiter().GetResult();

                Console.WriteLine("Published Event " + id);

                // Sleep as long as you need.
                Thread.Sleep(1000);
            }
        }

        private static EndpointConfiguration ConfigureNSB()
        {

            var endpointConfiguration = new EndpointConfiguration("NSB.Example.Publisher");
            var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            endpointConfiguration.SendFailedMessagesTo("error");

            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningEventsAs(
                type =>
                {
                    return type.Namespace != null &&
                           type.Namespace.EndsWith("Events") ;
                });

            endpointConfiguration.EnableInstallers();

            return endpointConfiguration;
        }
    }
}
