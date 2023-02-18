using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using CodeCodeChallenge.Tests.Integration.Helpers;
using CodeChallenge.Models;
using System.Text;
using System.Net;
using CodeCodeChallenge.Tests.Integration.Extensions;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var employee = new Employee()
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                FirstName = "John",
                LastName = "Lennon",
            };
            var compensation = new Compensation()
            {
                Employee = employee,
                Salary = 75000,
                EffectiveDate = new DateTime(2021, 08, 12)
            };

            var requestBody = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
                new StringContent(requestBody, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.CompensationId);
            Assert.AreEqual(employee.EmployeeId, newCompensation.Employee.EmployeeId);
            Assert.AreEqual(employee.FirstName, newCompensation.Employee.FirstName);
            Assert.AreEqual(employee.LastName, newCompensation.Employee.LastName);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
        }

        [TestMethod]
        public void GetCompensationById_Returns_Ok()
        {
            // Arrange
            var employee = new Employee()
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                FirstName = "John",
                LastName = "Lennon",
            };
            var compensation = new Compensation()
            {
                Employee = employee,
                Salary = 75000,
                EffectiveDate = new DateTime(2021, 08, 12)
            };

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{compensation.Employee.EmployeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensationResponse = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(compensationResponse.CompensationId);
            Assert.AreEqual(employee.EmployeeId, compensationResponse.Employee.EmployeeId);
            Assert.AreEqual(employee.FirstName, compensationResponse.Employee.FirstName);
            Assert.AreEqual(employee.LastName, compensationResponse.Employee.LastName);
            Assert.AreEqual(compensation.Salary, compensationResponse.Salary);
            Assert.AreEqual(compensation.EffectiveDate, compensationResponse.EffectiveDate);
        }
    }
}

