using RestaurantManager.Infrastructure.Repositories.Interfaces;

namespace RestaurantManager.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRestaurantRepository RestaurantRepository { get; }

        void SaveChanges();
    }
}
