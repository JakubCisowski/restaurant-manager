using RestaurantManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantManager.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        void Delete(TEntity entity);
        void Update(TEntity entity);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> FindMany(Expression<Func<TEntity, bool>> filter);
        bool RemoveOne(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter);
        Task AddAsync(TEntity entity);
        Task AddManyAsync(IEnumerable<TEntity> entity);
    }
}
