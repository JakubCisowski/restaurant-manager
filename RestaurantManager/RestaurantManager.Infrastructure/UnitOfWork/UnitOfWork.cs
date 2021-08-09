using RestaurantManager.Context;
using RestaurantManager.Infrastructure.Repositories.Interfaces;

namespace RestaurantManager.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantDbContext _dbContext;
        public IRestaurantRepository RestaurantRepository { get; }

        public UnitOfWork(RestaurantDbContext dbContext,
                          IRestaurantRepository restaurantRepository)
        {
            _dbContext = dbContext;
            RestaurantRepository = restaurantRepository;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
