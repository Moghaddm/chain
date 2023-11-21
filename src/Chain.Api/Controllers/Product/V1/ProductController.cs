using Chain.Application.Contract.Helpers.Api;
using Chain.Application.Contract.Models;
using Chain.Application.Contract.Ports.Services;
using Chain.Application.Interfaces;
using Chain.Application.Models;
using Chain.Application.Services;
using Chain.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Chain.Api.Controllers.Product.V1
{
    [ApiController]
    [Route("api/v{version:apiversion}/products")]
    [ApiVersion(ConstantHelper.ApiVersion)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;

        public ProductController(
            ICommentService commentService,
            IProductService productService,
            ICategoryService categoryService)
            => (_productService, _commentService, _categoryService) = (productService, commentService, categoryService);

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll();

            return Ok(products);
        }

        [HttpGet("{productId?}")]
        public async Task<IActionResult> GetById([FromRoute] Guid productId)
        {
            var product = await _productService.Get(productId);

            return product is not null ? Ok(product) : NotFound();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromForm] CreateEditProductDto input)
        {
            var productAdded = await _productService.Create(input);

            return Created(nameof(Index), productAdded);
        }

        [HttpPatch("{productId}/[action]")]
        public async Task<IActionResult> Modify([FromRoute] Guid productId, [FromBody] CreateEditProductDto product)
        {
            await _productService.Update(productId, product);

            return NoContent();
        }

        [HttpDelete("{productId}/[action]")]
        public async Task<IActionResult> Delete([FromRoute] Guid productId)
        {
            await _productService.Delete(productId);

            return NoContent();
        }

        [HttpGet("{productId}/comments")]
        public async Task<IActionResult> GetComments([FromRoute] Guid productId)
        {
            var comments = await _commentService.GetProductComments(productId);

            return Ok(comments);
        }

        [HttpPost("{productId}/[action]")]
        public async Task<IActionResult> CreateComment([FromRoute] Guid productId, [FromBody] CommentDto commentDto)
        {
            await _commentService.AddOnProduct(productId, commentDto);

            return NoContent();
        }
    }
}
