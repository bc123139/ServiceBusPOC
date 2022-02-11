using SericeBusPublisher.Dto;
using System.Threading.Tasks;

namespace SericeBusPublisher.MessagePublisher
{
    public interface IMessageBus
    {
        Task PublishMessage(ProductDto message, string topicName);
    }
}
