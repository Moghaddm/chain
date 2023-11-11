using Chain.Application.Contract.Models;
using Chain.Application.Contract.Ports.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chain.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet("{id}", Name = nameof(GetById) + "Categories")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var category = await _categoryService.Get(id);

            return category is null ? NotFound() : Ok(category);
        }

        [HttpGet(Name = nameof(Get) + "Categories")]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetAll();

            return Ok(categories);
        }

        [HttpPost("[action]", Name = nameof(Create) + "Categories")]
        public async Task<IActionResult> Create([FromBody] CategoryDto category)
        {
            var categoryId = await _categoryService.Create(category);

            return CreatedAtAction(nameof(GetById) + "Categories", categoryId);
        }

        [HttpPatch("{id}/[action]", Name = nameof(Modify) + "Categories")]
        public async Task<IActionResult> Modify([FromRoute] Guid id, [FromBody] CategoryDto category)
        {
            await _categoryService.Update(id, category);

            return NoContent();
        }

        [HttpDelete("{id}/[action]", Name = nameof(Delete) + "Categories")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _categoryService.Delete(id);

            return NoContent();
        }

        [HttpGet("{id}/Products", Name = nameof(GetProducts) + "Categories")]
        public async Task<IActionResult> GetProducts([FromRoute] Guid id)
        {
            var category = await _categoryService.GetCategoryProducts(id);

            return category is null ? NotFound() : Ok(category);
        }
    }
}
