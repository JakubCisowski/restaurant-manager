using RestaurantManager.Entities;
using RestaurantManager.Infrastructure.Repositories.Interfaces;

namespace RestaurantManager.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRestaurantRepository RestaurantRepository { get; }

        void SaveChanges();

        public IGenericRepository<T> GetRepository<T>() where T : Entity;
    }
}
