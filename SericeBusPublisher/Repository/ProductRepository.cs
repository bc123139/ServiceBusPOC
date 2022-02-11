using SericeBusPublisher.Dto;
using SericeBusPublisher.MessagePublisher;
using System.Threading.Tasks;

namespace SericeBusPublisher.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMessageBus _messageBus;
        public ProductRepository(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task<ProductDto> CreateProduct(ProductDto productDto)
        {
            if (productDto.Name.ToLower().Contains(" one"))
            {
                await _messageBus.PublishMessage(productDto, "producttopicone");
            }
            else
            {
                await _messageBus.PublishMessage(productDto, "producttopictwo");
            }
            

            return productDto;

        }

    }
}
