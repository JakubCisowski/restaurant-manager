using RestaurantManager.Context;
using RestaurantManager.Infrastructure.Repositories;
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

        public UnitOfWork(RestaurantDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

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
