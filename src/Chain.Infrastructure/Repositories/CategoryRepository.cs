using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Ports.Repositories;
using Chain.Domain.Entities;
using Chain.Infrastructure.Persistence;

namespace Chain.Infrastructure.Repositories
{
    public class CategoryRepository : BasicRepository<Category,Guid> , ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
