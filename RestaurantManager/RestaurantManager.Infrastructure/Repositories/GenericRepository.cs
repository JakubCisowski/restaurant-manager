using Microsoft.EntityFrameworkCore;
using RestaurantManager.Context;
using RestaurantManager.Entities;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

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

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<TEntity> FindMany(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Where(filter);
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> filter)
        {
            var entity = _dbSet.Where(filter)
                .FirstOrDefault();

            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public TEntity GetById(Guid id)
        {
            var entity = _dbSet.Find(id);
            return entity;
        }

        public bool RemoveOne(Expression<Func<TEntity, bool>> filter)
        {
            var targetEntity = _dbSet
                .Where(filter).FirstOrDefault(); ;

            if (targetEntity == null)
            {
                return false;
            }

            _dbSet.Remove(targetEntity);
            _dbContext.SaveChanges();
            return true;
        }

        public void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

    }
}
