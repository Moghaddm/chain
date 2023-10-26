using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Ports.Repositories;
using Chain.Application.Contract.Ports.Services;
using Chain.Domain.Entities;
using Chain.Infrastructure.Persistence;
using Chain.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ZstdSharp.Unsafe;

namespace Chain.Persistence.Repositories
{
    public class ProductMongoRepository : IProductRepository
    {
        private readonly IProductContext _context;

        public ProductMongoRepository(IProductContext context) => _context = context;

        public async ValueTask<bool> UpdateProduct(Guid id, Product product)
        {
            var updateResult = await _context
                .Products
                .ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task RemoveAsync(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p, product);

            await _context.Products.DeleteOneAsync(filter);
        }

        public async Task<Guid> CreateAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);

            return product.Id;
        }

        public async ValueTask<Product?> GetByIdAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(p => p.Id == id);

            return product == null ? null : await product.FirstOrDefaultAsync();
        }

        public async ValueTask<IList<Product?>> GetAsync()
            => _context.Products.FindAsync(_ => true).Result.ToList()!;
    }

    public class ProductEfRepository : BasicRepository<Product, Guid>, IProductRepository
    {
        public ProductEfRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
