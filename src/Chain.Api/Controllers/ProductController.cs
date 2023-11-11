using Chain.Application.Contract.Models;
using Chain.Application.Contract.Ports.Services;
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

            return products is not null ? Ok(products) : NotFound("There is not any products on data storage.");
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

            return product is not null ? Ok(product) : NotFound("There is not any products on data storage with expected argument.");
        }

        [HttpGet("{id}/comments", Name = nameof(GetComments) + "Products")]
        public async Task<IActionResult> GetComments([FromRoute] Guid id)
        {
            var comments = await _commentService.GetProductComments(id);

            return Ok(comments);
        }

        [HttpPost("{id}/[action]", Name = nameof(CreateComment) + "Products")]
        public async Task<IActionResult> CreateComment([FromRoute] Guid id, [FromBody] CommentDto commentDto)
        {
            await _commentService.AddOnProduct(id, commentDto);

            return NoContent();
        }

        [HttpPatch("{id}/[action]", Name = nameof(UpdateComment) + "Products")]
        public async Task<IActionResult> UpdateComment([FromRoute] Guid id, [FromBody] CommentDto commentDto)
        {
            await _commentService.UpdateOnProduct(id, commentDto);

            return NoContent();
        }

        [HttpDelete("{id}/[action]/{commentId}", Name = nameof(DeleteComment) + "Products")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id, [FromRoute] Guid commentId)
        {
            await _commentService.DeleteFromProduct(commentId,id);

            return NoContent();
        }

        [HttpGet("comments/{commentId}", Name = nameof(GetComment) + "Products")]
        public async Task<IActionResult> GetComment([FromRoute] Guid id)
        {
            var comments = await _commentService.GetById(id);

            return comments is not null ? Ok(comments) : NotFound("There is not any comments on data storage with expected product argument.");
        }
    }
}
