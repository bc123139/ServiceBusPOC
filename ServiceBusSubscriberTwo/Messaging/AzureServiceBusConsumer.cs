using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using ServiceBusSubscriberTwo.Messages;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusSubscriberTwo.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string planningdomesubscription;
        private readonly string productTopicTwo;
        private ServiceBusProcessor productProcessor;

        public AzureServiceBusConsumer()
        {
            serviceBusConnectionString = "Endpoint=sb://planningdomeservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=yyC4V63rZ33zGyaQbTvRaJ99cKS6N0O4smSmDDzl8NQ=";
            planningdomesubscription = "planningdomesubscription";
            productTopicTwo = "producttopictwo";

            var client = new ServiceBusClient(serviceBusConnectionString);
            productProcessor = client.CreateProcessor(productTopicTwo, planningdomesubscription);
        }

        public async Task Start()
        {
            productProcessor.ProcessMessageAsync += ProductMessageReceived;
            productProcessor.ProcessErrorAsync += ErrorHandler;
            await productProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await productProcessor.StopProcessingAsync();
            await productProcessor.DisposeAsync();
        }

        public async Task ProductMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            var productDto = JsonConvert.DeserializeObject<ProductDto>(body);
            Trace.WriteLine($"Product Id= { productDto.Id}, Product Name= {productDto.Name},Product Price= {productDto.Price}");
            await args.CompleteMessageAsync(args.Message);
        }

        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
