using SericeBusPublisher.Dto;
using System.Threading.Tasks;

namespace SericeBusPublisher.Repository
{
    public interface IProductRepository
    {
        Task<ProductDto> CreateProduct(ProductDto productDto);
    }
}
