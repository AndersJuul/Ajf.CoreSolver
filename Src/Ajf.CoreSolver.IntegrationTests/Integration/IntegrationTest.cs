using System.Net;
using Ajf.CoreSolver.IntegrationTests.Base;
using Ajf.CoreSolver.Models;
using AutoFixture;
using NUnit.Framework;
using RestSharp;

namespace Ajf.CoreSolver.IntegrationTests.Integration
{
    [TestFixture]
    public class IntegrationTest : BaseIntegrationTest
    {
        [Test]
        [Category("Integration")]
        public void ThatPostingValidCalculationIsSucces()
        {
            // Arrange
            var client = GetRestClient();
            var request = new RestRequest("api/calculation", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddBody(Fixture.Build<CalculationRequest>().Create());

            // Act
            var response = client.Execute<CalculationResponse>(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Success was expected, got: " +response);
        }
    }
}