using Microsoft.EntityFrameworkCore;
using RestaurantManager.Context;
using RestaurantManager.Entities;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantManager.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly RestaurantDbContext _dbContext;

        public GenericRepository(RestaurantDbContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
            _dbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddManyAsync(IEnumerable<TEntity> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<TEntity> FindMany(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Where(filter).AsNoTracking();
        }

        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter)
        {
            var entity = await _dbSet.Where(filter)
                .FirstOrDefaultAsync();

            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public bool RemoveOne(Expression<Func<TEntity, bool>> filter)
        {
            var targetEntity = _dbSet
                .Where(filter).FirstOrDefault();

            if (targetEntity == null)
            {
                return false;
            }

            _dbSet.Remove(targetEntity);
            return true;
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
            //_dbContext.Entry(entity).State = EntityState.Modified;
        }

    }
}
