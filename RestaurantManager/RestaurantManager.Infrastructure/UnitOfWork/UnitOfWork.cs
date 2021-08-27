using RestaurantManager.Context;
using RestaurantManager.Entities;
using RestaurantManager.Infrastructure.Repositories;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using System.Threading.Tasks;

namespace RestaurantManager.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantDbContext _dbContext;

        public IRestaurantRepository RestaurantRepository { get; }
        public IOrderRepository OrderRepository { get; }

        public UnitOfWork(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
            OrderRepository = new OrderRepository(_dbContext);
            RestaurantRepository = new RestaurantRepository(_dbContext);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IGenericRepository<T> GetRepository<T>() where T : Entity
        {
            return new GenericRepository<T>(_dbContext);
        }
    }
}
