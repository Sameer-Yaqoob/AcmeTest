using AcmeCorpAPI.Controllers;
using AcmeCorpAPI.DBContext;
using AcmeCorpAPI.Interfaces;
using AcmeCorpAPI.Models;
using AcmeCorpAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AcmeCorpAPI.Tests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private CustomerRepository _repositry;
        private Mock<AcmeCorpDBContext> _mockContext;

        [SetUp]
        public void Setup()
        {

            _mockContext = new Mock<AcmeCorpDBContext>();
            _repositry = new CustomerRepository(_mockContext.Object);
        }
        [Test]
        public void GetCustomer_ShouldReturnOnResult() 
        {

            var customerData = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Address = "test",
                    Age = 10,
                    Email = "test@test.com",
                    Gender = "Male",
                    Name = "JohnDoe",
                    PhoneNumber = "12345678",
                    Orders = new List<Order>
                    {
                        new Order
                        {
                             CustomerId = 1,
                              Id =1,
                               OrderDate =DateTime.Now,
                                ProductName ="Product1"
                        },
                        new Order
                        {
                             CustomerId = 1,
                              Id =2,
                               OrderDate =DateTime.Now,
                                ProductName ="Product2"
                        },
                    }
                },
                 new Customer
                {
                    Id = 2,
                    Address = "test2",
                    Age = 10,
                    Email = "test2@test.com",
                    Gender = "Female",
                    Name = "Jane",
                    PhoneNumber = "87654321",
                    Orders = new List<Order>
                    {
                        new Order
                        {
                             CustomerId = 2,
                              Id =1,
                               OrderDate =DateTime.Now,
                                ProductName ="Product1"
                        },
                        new Order
                        {
                             CustomerId = 2,
                              Id =2,
                               OrderDate =DateTime.Now,
                                ProductName ="Product2"
                        },
                    }
                }
            };

           // _mockContext.Setup(x => x.Customers).Returns(customerData);
            var result = _repositry.GetAllCustomers();
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }
    }
}
