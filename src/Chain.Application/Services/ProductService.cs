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

namespace Chain.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentService _commentService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository,
            ICompanyRepository companyRepository,
            ICommentService commentService)
            => (_productRepository, _unitOfWork, _categoryRepository, _companyRepository, _commentService)
                = (productRepository, unitOfWork, categoryRepository, companyRepository, commentService);

        public async Task<Guid> Create(CreateEditProductDto createProductDto)
        {
            try
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
                    createProductDto.Attachments);

                var productCreate = await _productRepository.CreateAsync(product);

                await _unitOfWork.SaveChangesAsync();

                return productCreate;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

                throw;
            }
        }

        public async ValueTask<List<ProductDto>> GetAll()
        {
            var products = await _productRepository.GetAsync();

            return products.Select(p => new ProductDto(p.Name,
                p.FullEnglishName,
                p.Description,
                p.Quantity,
                p.Price,
                new CompanyDto(p.Company.Name),
                new CategoryDto(p.Category.Title, p.Category.LimitOrder),
                p.Attachments)).ToList();
        }

        public async ValueTask<OneProductDto> GetById(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            var comments = await _commentService.GetProductComments(id);

            var productDto = new OneProductDto(
                product.Name,
                product.FullEnglishName,
                product.Description,
                product.Quantity,
                product.Price,
                new CompanyDto(product.Company.Name),
                new CategoryDto(product.Category.Title, product.Category.LimitOrder),
                comments,
                product.Attachments);

            return productDto;
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);

                await _productRepository.RemoveAsync(product);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

                throw;
            }
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
            try
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
            catch (Exception exception)
            {
                Console.WriteLine(exception);

                throw;
            }
        }
    }
}
