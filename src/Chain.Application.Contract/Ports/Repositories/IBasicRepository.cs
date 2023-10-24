using Chain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Domain;

namespace Chain.Application.Contract.Ports.Repositories
{
    public interface IBasicRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {
        Task RemoveAsync(TEntity entity);
        Task<TKey> CreateAsync(TEntity entity);
        ValueTask<TEntity?> GetByIdAsync(TKey id);
        ValueTask<IList<TEntity?>> GetAsync();
    }
}
