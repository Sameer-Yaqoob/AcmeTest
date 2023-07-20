using AcmeCorpAPI.Models;

namespace AcmeCorpAPI.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerById(int id);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(int id);
    }
}
