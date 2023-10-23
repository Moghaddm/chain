using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Domain.Entities;
using MongoDB.Driver;

namespace Chain.Infrastructure.Persistence.Products
{
    public class ProductMongoSeedData
    {
        public static void SeedData(IMongoCollection<Product> products)
        {
            if (!products.Find(_ => true).Any())
                products.InsertMany(GetProducts());
        }

        public static IEnumerable<Product> GetProducts() => new List<Product>
        {
            new Product("Laptop",
                "High-performance laptop with SSD",
                "Electronics",
                1200,
                10,
                new Company("Tech Corp"),
                new Category("Electronics", 5)),
            new Product("Smartphone",
                "Latest smartphone with great camera",
                "Electronics",
                800,
                15,
                new Company("Gadget Co"),
                new Category("Electronics",
                    5)),
            new Product("Coffee Maker",
                "Premium coffee maker with timer",
                "Kitchen Appliances",
                150,
                25,
                new Company("Kitchen Gadgets Inc"),
                new Category("Kitchen Appliances",
                    4)),
            new Product("Running Shoes",
                "Comfortable running shoes for athletes",
                "Sports & Fitness",
                80,
                30,
                new Company("Athletic Gear Ltd"),
                new Category("Sports & Fitness", 6)),
            new Product("Bookshelf",
                "Wooden bookshelf with adjustable shelves",
                "Furniture",
                250,
                8,
                new Company("Furniture Emporium"),
                new Category("Furniture",
                    7)),
            new Product("Bluetooth Speaker",
                "Portable Bluetooth speaker for music lovers",
                "Electronics",
                50,
                40,
                new Company("Sound Systems Co"),
                new Category("Electronics",
                    5)),
            new Product("Cookware Set",
                "Complete non-stick cookware set",
                "Kitchen Appliances",
                200,
                12,
                new Company("Kitchen Gadgets Inc"),
                new Category("Kitchen Appliances",
                    4)),
            new Product("Mountain Bike",
                "Durable mountain bike for adventurous riders",
                "Sports & Fitness",
                450,
                5,
                new Company("Adventure Sports Gear"),
                new Category("Sports & Fitness", 6)),
        };
    }
}
