using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace AcmeCorpAPI.Tests
{
	
        [TestFixture]
        public class CustomersIntegrationTests
        {
            private TestServer _testServer;
            private HttpClient _httpClient;

            [SetUp]
            public void Setup()
            {
                // Create a test server with your API startup class
                _testServer = new TestServer(new WebHostBuilder()
                    .UseStartup<IStartup>());

                // Create a test client to interact with the API
                _httpClient = _testServer.CreateClient();
            }

            [Test]
            public async Task GetCustomers_ShouldReturnOkResult()
            {
                // Arrange
                var requestUrl = "/api/customers";

                // Act
                var response = await _httpClient.GetAsync(requestUrl);

                // Assert
                response.EnsureSuccessStatusCode(); // This will throw an exception if the status code is not 2xx
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            // Add more integration tests for other controller actions...
        }
    
}

