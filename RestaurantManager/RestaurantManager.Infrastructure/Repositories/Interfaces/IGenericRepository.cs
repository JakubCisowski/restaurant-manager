using RestaurantManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        TEntity GetById(Guid id);
        IQueryable<TEntity> GetAll();
        TEntity FindOne(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> FindMany(Expression<Func<TEntity, bool>> filter);
        bool RemoveOne(Expression<Func<TEntity, bool>> filter);
    }
}
