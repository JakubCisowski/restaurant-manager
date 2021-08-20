using Microsoft.EntityFrameworkCore;
using RestaurantManager.Context;
using RestaurantManager.Entities.Orders;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private DbSet<Order> _orderSet;

        public OrderRepository(RestaurantDbContext dbContext) : base(dbContext)
        {
            _orderSet = dbContext.Set<Order>();
        }

        public Task<Order> FindOneOrder(int orderNo, string phoneNumber, params Expression<Func<Order, object>>[] includes)
        {
            var query = _orderSet.AsQueryable();

            foreach (var include in includes)
            {
                query = includes.Aggregate(query, (q, w) => q.Include(w));
            }


            return query
                .FirstOrDefaultAsync(x => x.OrderNo == orderNo && x.Customer.Phone == phoneNumber);
        }

        public IQueryable<Order> FindOrders(int orderNo, string phoneNumber)
        {
            return _orderSet
                .Where(x => x.OrderNo == orderNo && x.Customer.Phone == phoneNumber)
                .AsNoTracking();
        }
    }
}
