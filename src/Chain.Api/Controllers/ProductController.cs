using Chain.Application.Contract.Models.Api;
using Chain.Application.Interfaces;
using Chain.Application.Models;
using Chain.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Chain.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) => _productService = productService;

        [HttpGet(Name = nameof(Get))]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAll();

            return Ok(products);
        }

        [HttpPost("[action]", Name = nameof(Create))]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest input)
        {
            var productAdded = await _productService.Create(input.Product,input.CompanyId,input.CategoryId);

            return CreatedAtAction(nameof(GetById), productAdded);
        }

        [HttpPatch("[action]/{id}", Name = nameof(Modify))]
        public async Task<IActionResult> Modify([FromRoute] Guid id, [FromBody] ProductDto product)
        {
            await _productService.Update(id, product);

            return NoContent();
        }

        [HttpDelete("[action]/{id}", Name = nameof(Delete))]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _productService.Delete(id);

            return NoContent();
        }

        [HttpGet("[action]/{id}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var product = await _productService.GetById(id);

            return Ok(product);
        }
    }
}
