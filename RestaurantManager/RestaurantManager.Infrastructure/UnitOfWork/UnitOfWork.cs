using RestaurantManager.Context;
using RestaurantManager.Infrastructure.Repositories;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantDbContext _dbContext;

        public UnitOfWork(RestaurantDbContext dbContext,
                          IRestaurantRepository restaurantRepository)
        {
            _dbContext = dbContext;
            RestaurantRepository = restaurantRepository;
        }

        public IRestaurantRepository RestaurantRepository { get; }

        public object GetRepository()
        {
            return null;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
