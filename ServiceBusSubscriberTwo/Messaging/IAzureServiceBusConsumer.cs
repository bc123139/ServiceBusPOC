using System.Threading.Tasks;

namespace ServiceBusSubscriberTwo.Messaging
{
    public interface IAzureServiceBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
