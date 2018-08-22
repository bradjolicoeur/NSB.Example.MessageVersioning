using NSB.Example.Contracs.Events;
using NServiceBus;
using NServiceBus.Persistence.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NSB.Example.Publisher
{
    class Program
    {
        static void Main()
        {
            EndpointConfiguration endpointConfiguration = ConfigureNSB();

            var endpointInstance = Endpoint.Start(endpointConfiguration)
              .GetAwaiter().GetResult();

            while (true)
            {
                endpointInstance.Publish<IExampleEventV2>(
                        messageConstructor: message =>
                        {
                            message.Message = "Hello world: " + Guid.NewGuid().ToString();
                            message.NewProperty = "Populated with: " + Guid.NewGuid().ToString();
                        })
                 .GetAwaiter().GetResult();

                Console.WriteLine("Published Event");

                // Sleep as long as you need.
                Thread.Sleep(1000);
            }
        }

        private static EndpointConfiguration ConfigureNSB()
        {

            var endpointConfiguration = new EndpointConfiguration("NSB.Example.Publisher");
            var persistence = endpointConfiguration.UsePersistence<NHibernatePersistence>();
            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
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
