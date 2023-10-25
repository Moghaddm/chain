using Chain.Application.Contract.Ports.Services;
using Chain.Application.Interfaces;
using Chain.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chain.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;

        public ProductController(IProductService productService,
            ICommentService commentService,
            ICategoryService categoryService)
            => (_productService, _commentService, _categoryService) = (productService, commentService, categoryService);

        [HttpGet(Name = nameof(Get) + "Products")]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAll();

            return Ok(products);
        }

        [HttpPost("[action]", Name = nameof(Create) + "Products")]
        public async Task<IActionResult> Create([FromBody] CreateEditProductDto input)
        {
            var productAdded = await _productService.Create(input);

            return CreatedAtAction(nameof(GetById), productAdded);
        }

        [HttpPatch("{id}/[action]", Name = nameof(Modify) + "Products")]
        public async Task<IActionResult> Modify([FromRoute] Guid id, [FromBody] CreateEditProductDto product)
        {
            await _productService.Update(id, product);

            return NoContent();
        }

        [HttpDelete("{id}/[action]", Name = nameof(Delete) + "Products")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _productService.Delete(id);

            return NoContent();
        }

        [HttpGet("{id}", Name = nameof(GetById) + "Products")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var product = await _productService.GetById(id);

            return Ok(product);
        }

        [HttpGet("{id}/comments", Name = nameof(GetComments) + "Products")]
        public async Task<IActionResult> GetComments([FromRoute] Guid id)
        {
            var comments = await _commentService.GetProductComments(id);

            return Ok(comments);
        }
    }
}
