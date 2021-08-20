using RestaurantManager.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        IQueryable<Order> FindOrders(int orderNo, string phoneNumber);
        Task<Order> FindOneOrder(int orderNo, string phoneNumber, params System.Linq.Expressions.Expression<Func<Order, object>>[] includes);
    }
}
