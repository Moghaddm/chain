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
    public class CompanyRepository : BasicRepository<Company, Guid> , ICompanyRepository
    {
        public CompanyRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
