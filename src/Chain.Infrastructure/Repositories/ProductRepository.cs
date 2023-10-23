using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Ports.Repositories;
using Chain.Application.Contract.Ports.Services;
using Chain.Domain.Entities;
using Chain.Infrastructure.Persistence;
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

        public async ValueTask RemoveProduct(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p, product);

            await _context.Products.DeleteOneAsync(filter);
        }

        public async ValueTask<Guid> CreateAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);

            return product.Id;
        }

        public async ValueTask<Product?> GetByIdAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(p => p.Id == id);

            return product == null ? null : await product.FirstOrDefaultAsync();
        }

        public async ValueTask<List<Product?>> GetAsync()
            => _context.Products.FindAsync(_ => true).Result.ToList()!;
    }

    public class ProductEfRepository : IProductRepository
    {
        private readonly DbSet<Product> _products;

        public ProductEfRepository(ChainDbContext context)
            => _products = context.Set<Product>();

        public async ValueTask RemoveProduct(Product product) => _products.Remove(product);

        public async ValueTask<Guid> CreateAsync(Product product)
        {
            var addedProduct = await _products.AddAsync(product);

            return addedProduct.Entity.Id;
        }

        public async ValueTask<Product?> GetByIdAsync(Guid id)
            => await _products.FirstOrDefaultAsync(p => p.Id == id);

        public async ValueTask<List<Product?>> GetAsync() => await _products.ToListAsync();
    }
}
