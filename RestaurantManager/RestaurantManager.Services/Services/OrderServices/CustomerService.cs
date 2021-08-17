using RestaurantManager.Entities.Orders;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Customer> _customerRepository;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = _unitOfWork.GetRepository<Customer>();
        }

        public async Task<Customer> CreateCustomer(string phone)
        {
            var customer = new Customer(phone);
            await _customerRepository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> GetCustomer(string phone)
        {
            var customer = await _customerRepository.FindOneAsync(x => x.Phone == phone);
            return customer;
        }
    }
}
