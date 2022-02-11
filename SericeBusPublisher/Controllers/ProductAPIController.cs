using Microsoft.AspNetCore.Mvc;
using SericeBusPublisher.Dto;
using SericeBusPublisher.Repository;
using System.Threading.Tasks;

namespace SericeBusPublisher.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductAPIController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            var result = await _productRepository.CreateProduct(productDto);
            return Ok(result);
        }
    }
}
