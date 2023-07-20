using AcmeCorpAPI.Interfaces;
using AcmeCorpAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcmeCorpAPI.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        public readonly ICustomerRepository _icustomer;
        public CustomersController(ICustomerRepository icustomer) 
        { 
            _icustomer = icustomer;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _icustomer.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]

        public IActionResult GetCustomerById(int id)
        {
            var customer = _icustomer.GetCustomerById(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            _icustomer.AddCustomer(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }
    }
}
