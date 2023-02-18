using System;
using System.Net;
using System.Net.Http;
using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration{
    [TestClass]
    public class ReportingStructureControllerTests{
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context){
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest(){
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void GetReportingStructureById_Returns_Ok(){
            var empId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var getRequestTask = _httpClient.GetAsync($"api/reporting/{empId}");
            var response = getRequestTask.Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var structure = response.DeserializeContent<ReportingStructure>();
            Assert.IsNotNull(structure.ReportingStructureId);
            Assert.AreEqual("John", structure.Employee.FirstName);
            Assert.AreEqual("Lennon", structure.Employee.LastName);
            Assert.AreEqual(empId, structure.Employee.EmployeeId);
            Assert.AreEqual(4, structure.NumberOfReport);
        }
    }
}

