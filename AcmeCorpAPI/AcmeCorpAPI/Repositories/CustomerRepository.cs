using AcmeCorpAPI.DBContext;
using AcmeCorpAPI.Interfaces;
using AcmeCorpAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeCorpAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AcmeCorpDBContext _dbContext;

        public CustomerRepository(AcmeCorpDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Customer> GetCustomerById(int id)
        {
            var customer = _dbContext.Customers.Include(c => c.Orders).FirstOrDefault(c => c.Id == id);
            return Task.FromResult(customer);
        }

        public Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = _dbContext.Customers.Include(c => c.Orders).ToList();
            return Task.FromResult<IEnumerable<Customer>>(customers);
        }

        public async Task AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
            }
            var customerExest= _dbContext.Customers.FirstOrDefault(c=> c.Id == customer.Id);

            if (customerExest!=null)
            {
                throw new ArgumentException(nameof(customer), "Customer Already Exist.");
            }

            _dbContext.Customers.Add(customer);
            
            foreach(var order in  customer.Orders)
            {
                _dbContext.Orders.Add(order);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
            }

            var existingCustomer = await _dbContext.Customers.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Age = customer.Age;
                existingCustomer.Gender = customer.Gender;
                existingCustomer.Address = customer.Address;
                existingCustomer.Email = customer.Email;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                // Update or add orders if needed
                existingCustomer.Orders = customer.Orders;

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteCustomer(int id)
        {
            var customerToRemove = await _dbContext.Customers.FindAsync(id);
            if (customerToRemove != null)
            {
                _dbContext.Customers.Remove(customerToRemove);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
