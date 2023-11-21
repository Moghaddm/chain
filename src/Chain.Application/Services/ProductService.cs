using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Models;
using Chain.Application.Contract.Ports.Repositories;
using Chain.Application.Contract.Ports.Services;
using Chain.Application.Interfaces;
using Chain.Application.Models;
using Chain.Domain.Entities;
using Chain.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Chain.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository,
            ICompanyRepository companyRepository)
            => (_productRepository, _unitOfWork, _categoryRepository, _companyRepository)
                = (productRepository, unitOfWork, categoryRepository, companyRepository);

        public async Task<Guid> Create(CreateEditProductDto createProductDto)
        {
            var category = await _categoryRepository.GetByIdAsync(createProductDto.CategoryId);

            var company = await _companyRepository.GetByIdAsync(createProductDto.CompanyId);

            Product product = new(createProductDto.Name,
                 createProductDto.FullEnglishName,
                 createProductDto.Description,
                 createProductDto.Price,
                 createProductDto.Quantity,
                 company,
                 category,
                 createProductDto.Attachments.Select(a =>
                 {
                     using (var memoryStream = new MemoryStream())
                     {
                         a.Image.CopyTo(memoryStream);
                         return new Attachment(memoryStream.ToArray(), a.Alt!, a.ImageTitle!, a.ImageMimeType);
                     }
                 }).ToList());

            var productCreate = await _productRepository.CreateAsync(product);

            await _unitOfWork.SaveChangesAsync();

            return productCreate;
        }

        public async ValueTask<IEnumerable<ProductDto>> GetAll()
        {
            var products = await _productRepository.GetAsync();

            if (products is null)
                return Enumerable.Empty<ProductDto>();

            return products.Select(p => new ProductDto(
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

        public async ValueTask<OneProductDto> Get(Guid id)
        {
            var rateService = new RateService();

            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
                return null;

            var comments = product.Comments;

            if (comments is null)
                comments = Enumerable.Empty<Comment>().ToList();

            var productDto = new OneProductDto(
                product.Id,
                product.Name,
                product.FullEnglishName,
                product.Description,
                product.Quantity,
                product.Price,
                new RateModel(rateService.GetAverageRate(comments), rateService.GetPercentRateNumbers(comments)),
                new CompanyDto(product.Company.Id, product.Company.Name),
                new CategoryDto(product.Category.Id, product.Category.Title, product.Category.LimitOrder),
                comments.Select(c => new CommentDto(
                c.Id,
                c.WriterAlias,
                c.Title,
                c.Description,
                c.Gmail,
                c.DateTimeCommented,
                c.Suggest,
                c.VoteUps,
                c.VoteDowns,
                c.RateNumber)),
                product.Attachments.Select(a =>
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        var attachment = new AttachmentDto(new FormFile(memoryStream, 0, a.Image.Length, null, a.ImageTitle), a.ImageMimeType, a.Alt, a.ImageTitle);

                        return attachment;
                    }
                }).ToList());

            return productDto;
        }

        public async Task Delete(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            await _productRepository.RemoveAsync(product);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCompany(Guid id, Guid companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);

            var product = await _productRepository.GetByIdAsync(id);

            product!.UpdateCompany(company!);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCategory(Guid id, Guid categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            var product = await _productRepository.GetByIdAsync(id);

            product!.UpdateCategory(category!);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Update(Guid id, CreateEditProductDto editProductDto)
        {
            var product = await _productRepository.GetByIdAsync(id);

            product.UpdateProduct(
                editProductDto.Name,
                editProductDto.FullEnglishName,
                editProductDto.Description,
                editProductDto.Price,
                editProductDto.Quantity);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
