using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Ports.Repositories;
using Chain.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chain.Infrastructure.Repositories
{
    public class BasicRepository<TEntity, TKey> : IBasicRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {
        private readonly DbSet<TEntity> _entities;

        public BasicRepository(IUnitOfWork context)
            => _entities = context.Set<TEntity>();

        public async Task RemoveAsync(TEntity entity)
            => _entities.Remove(entity);

        public async Task<TKey> CreateAsync(TEntity entity)
        {
            var addedEntity = await _entities.AddAsync(entity);

            return addedEntity.Entity.Id;
        }

        public async ValueTask<TEntity?> GetByIdAsync(TKey id)
            => await _entities.FindAsync(id);

        public async ValueTask<IList<TEntity?>> GetAsync() => await _entities.ToListAsync();
    }
}
