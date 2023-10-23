using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Ports.Services;
using MongoDB.Driver;
using Chain.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Chain.Infrastructure.Persistence.Products
{
    public class ProductMongoContext : IProductContext
    {
        public ProductMongoContext(IConfiguration dbConfiguration)
        {
             dbConfiguration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var mongoClient = new MongoClient(dbConfiguration
                .GetValue<string>("ProductDatabaseSettings:ConnectionString"));

            var mongoDatabase = mongoClient.GetDatabase(dbConfiguration
                .GetValue<string>("ProductDatabaseSettings:DatabaseName"));

            Products = mongoDatabase.GetCollection<Product>(dbConfiguration
                .GetValue<string>("ProductDatabaseSettings:CollectionName"));

            ProductMongoSeedData.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; set; }
    }
}
