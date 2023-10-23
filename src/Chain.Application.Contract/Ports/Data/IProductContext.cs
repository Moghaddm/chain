using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Domain.Entities;
using MongoDB.Driver;

namespace Chain.Application.Contract.Ports.Services
{
    public interface IProductContext
    {
        public IMongoCollection<Product> Products { get; set; }
    }
}
