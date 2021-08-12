using RestaurantManager.Entities;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using System.Threading.Tasks;

namespace RestaurantManager.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRestaurantRepository RestaurantRepository { get; }

        public IGenericRepository<T> GetRepository<T>() where T : Entity;
        Task SaveChangesAsync();
    }
}
