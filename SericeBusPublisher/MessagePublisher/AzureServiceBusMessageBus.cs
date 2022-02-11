using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using Newtonsoft.Json;
using SericeBusPublisher.Dto;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SericeBusPublisher.MessagePublisher
{
    public class AzureServiceBusMessageBus : IMessageBus
    {
        private readonly string connectionString = "Endpoint=sb://planningdomeservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=yyC4V63rZ33zGyaQbTvRaJ99cKS6N0O4smSmDDzl8NQ=";
        private readonly string planningdomesubscription = "planningdomesubscription";
        public async Task PublishMessage(ProductDto message, string topicName)
        {
            var managementClient = new ManagementClient(connectionString);
            if(!await managementClient.TopicExistsAsync(topicName))
            {
                await managementClient.CreateTopicAsync(topicName); 
            }

            if(!await managementClient.SubscriptionExistsAsync(topicName, planningdomesubscription))
            {
                await managementClient.CreateSubscriptionAsync(new SubscriptionDescription(topicName, planningdomesubscription));
            }
            

            await using var client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(topicName);

            var jsonMessage = JsonConvert.SerializeObject(message);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await sender.SendMessageAsync(finalMessage);

            await client.DisposeAsync();
        }
    }
}
