using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<Guid> Create(ProductDto productDto, Guid companyId, Guid categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);

                var company = await _companyRepository.GetByIdAsync(companyId);

                Product product = new(productDto.Name,
                    productDto.FullEnglishName,
                    productDto.Description,
                    productDto.Price,
                    productDto.Quantity,
                    company,
                    category,
                    productDto.Attachments);

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
                p.Company, 
                p.Category,
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
                product.Company,
                product.Category,
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

        public async Task Update(Guid id, OneProductDto productDto)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);

                product.UpdateProduct(
                    productDto.Name,
                    productDto.FullEnglishName,
                    productDto.Description,
                    productDto.Price,
                    productDto.Quantity,
                    productDto.Company,
                    productDto.Category);

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
