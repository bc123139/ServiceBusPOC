using System.Threading.Tasks;

namespace ServiceBusSubscriberOne.Messaging
{
    public interface IAzureServiceBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
