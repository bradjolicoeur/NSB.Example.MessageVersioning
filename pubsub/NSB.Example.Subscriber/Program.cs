using NSB.Example.Contracs.Events;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB.Example.Subscriber
{
    class Program
    {
        static void Main()
        {
            EndpointConfiguration endpointConfiguration = ConfigureNSB();

            var endpointInstance = Endpoint.Start(endpointConfiguration)
              .GetAwaiter().GetResult();

            do
            {

            } while (true);
        }

        private static EndpointConfiguration ConfigureNSB()
        {

            var endpointConfiguration = new EndpointConfiguration("NSB.Example.Subscriber");
            endpointConfiguration.UsePersistence<NHibernatePersistence>();
            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.SendFailedMessagesTo("error");

            var routing = transport.Routing();
            routing.RegisterPublisher(typeof(IExampleEvent), "NSB.Example.Publisher");

            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningEventsAs(
                type =>
                {
                    return type.Namespace != null &&
                           type.Namespace.EndsWith("Events");
                });

            endpointConfiguration.EnableInstallers();

            return endpointConfiguration;
        }
    }
}
