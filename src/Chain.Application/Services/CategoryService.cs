using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Models;
using Chain.Application.Contract.Ports.Repositories;
using Chain.Application.Contract.Ports.Services;
using Chain.Application.Models;
using Chain.Domain.Entities;

namespace Chain.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;


        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
            => (_categoryRepository, _unitOfWork) = (categoryRepository, unitOfWork);

        public async Task<Guid> Create(CategoryDto categoryDto)
        {
            Category category = new(categoryDto.Title, categoryDto.LimitOrder);

            await _categoryRepository.CreateAsync(category);

            await _unitOfWork.SaveChangesAsync();

            return category.Id;
        }

        public async Task Update(Guid id, CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            category.Update(categoryDto.Title, categoryDto.LimitOrder);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            await _categoryRepository.RemoveAsync(category);

            await _unitOfWork.SaveChangesAsync();
        }

        public async ValueTask<CategoryShowProductsDto> Get(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            var productsDto = category.Products.Select(c => new ProductDto(
                c.Name,
                c.FullEnglishName,
                c.Description,
                c.Quantity,
                c.Price,
                new CompanyDto(c.Company.Name),
                new CategoryDto(c.Category.Title, c.Category.LimitOrder),
                c.Attachments));

            return new CategoryShowProductsDto(category.Title, category.LimitOrder, productsDto);
        }

        public async ValueTask<IEnumerable<CategoryDto>> GetAll()
        {
            var categories = await _categoryRepository.GetAsync();

            return categories.Select(c => new CategoryDto(c.Title, c.LimitOrder));
        }

        public async ValueTask<IEnumerable<ProductDto>> GetCategoryProducts(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            return category.Products.Select(p => new ProductDto(
                p.Name,
                p.FullEnglishName,
                p.Description,
                p.Quantity,
                p.Price,
                new CompanyDto(p.Company.Name),
                new CategoryDto(p.Category.Title, p.Category.LimitOrder),
                p.Attachments));
        }
    }
}
