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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Caching.Distributed;

namespace Chain.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _distributedCache;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IDistributedCache distributedCache)
            => (_categoryRepository, _unitOfWork, _distributedCache) = (categoryRepository, unitOfWork, distributedCache);

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

        public async ValueTask<CategoryDto> Get(Guid id)
        {
            //_distributedCache.GetAsync();
            var category = await _categoryRepository.GetByIdAsync(Guid.Parse(id.ToString()!));

            if (category == null)
                return null;

            return new CategoryDto(id, category.Title, category.LimitOrder);
        }

        public async ValueTask<IEnumerable<CategoryDto>> GetAll()
        {
            var categories = await _categoryRepository.GetAsync();

            if (categories == null)
                return Enumerable.Empty<CategoryDto>();

            return categories.Select(c => new CategoryDto(c.Id, c.Title, c.LimitOrder));
        }

        public async ValueTask<IEnumerable<ProductDto>> GetCategoryProducts(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category!.Products is null)
                return Enumerable.Empty<ProductDto>();

            return category.Products.Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.FullEnglishName,
                p.Description,
                p.Quantity,
                p.Price,
                new CompanyDto(p.Company.Id, p.Company.Name),
                new CategoryDto(p.Category.Id, p.Category.Title, p.Category.LimitOrder),
                p.Attachments.Select(a =>
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        var attachment = new AttachmentDto(new FormFile(memoryStream, 0, a.Image.Length, null, a.ImageTitle), a.ImageMimeType, a.Alt, a.ImageTitle);

                        return attachment;
                    }
                }).ToList()));
        }
    }
}
