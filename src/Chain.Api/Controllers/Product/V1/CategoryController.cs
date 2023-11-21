using Chain.Application.Contract.Helpers.Api;
using Chain.Application.Contract.Models;
using Chain.Application.Contract.Ports.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chain.Api.Controllers.Product.V1
{
    [ApiController]
    [Route("api/v{version:apiversion}/categories")]
    [ApiVersion(ConstantHelper.ApiVersion)]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAll();

            return Ok(categories);
        }

        [HttpGet("{categoryId:guid?}")]
        public async Task<IActionResult> GetById([FromRoute] Guid categoryId)
        {
            var category = await _categoryService.Get(categoryId);

            return category is null ? NotFound() : Ok(category);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CategoryDto category)
        {
            var categoryId = await _categoryService.Create(category);

            return Created(nameof(Index), categoryId);
        }

        [HttpPatch("{categoryId}/[action]")]
        public async Task<IActionResult> Modify([FromRoute] Guid categoryId, [FromBody] CategoryDto category)
        {
            await _categoryService.Update(categoryId, category);

            return NoContent();
        }

        [HttpDelete("{categoryId}/[action]")]
        public async Task<IActionResult> Delete([FromRoute] Guid categoryId)
        {
            await _categoryService.Delete(categoryId);

            return NoContent();
        }

        [HttpGet("{categoryId}/Products")]
        public async Task<IActionResult> GetProducts([FromRoute] Guid categoryId)
        {
            var category = await _categoryService.GetCategoryProducts(categoryId);

            return category is null ? NotFound() : Ok(category);
        }
    }
}
