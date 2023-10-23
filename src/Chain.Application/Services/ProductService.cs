using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Ports.Repositories;
using Chain.Application.Interfaces;
using Chain.Application.Models;
using Chain.Domain.Entities;

namespace Chain.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
            => (_productRepository, _unitOfWork) = (productRepository, unitOfWork);

        public async Task<Guid> Create(ProductDto productDto)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            try
            {
                Product product = new(productDto.Name,
                    productDto.FullEnglishName,
                    productDto.Description,
                    productDto.Price,
                    productDto.Quantity,
                    new Company(""),
                    new Category("", 1));

                var productCreate = await _productRepository.CreateAsync(product);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return productCreate;
            }
            catch (Exception exception)
            {
                cancellationTokenSource.Cancel();

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
                p.Price, p.Company, p.Category)).ToList();
        }

        public async ValueTask<ProductDto> GetById(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            var productDto = new ProductDto(
                product.Name,
                product.FullEnglishName,
                product.Description,
                product.Quantity,
                product.Price,
                product.Company,
                product.Category);

            return productDto;
        }

        public async Task Delete(Guid id)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            try
            {
                var product = await _productRepository.GetByIdAsync(id);

                await _productRepository.RemoveProduct(product);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                cancellationTokenSource.Cancel();

                Console.WriteLine(exception);

                throw;
            }
        }

        public async Task Update(Guid id, ProductDto productDto)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            try
            {
                var product = await _productRepository.GetByIdAsync(id);

                product.UpdateProduct(productDto.Name,
                    productDto.FullEnglishName, 
                    productDto.Description, 
                    productDto.Price, 
                    productDto.Quantity, 
                    productDto.Company, 
                    productDto.Category);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

                throw;
            }
        }
    }
}
