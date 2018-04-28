using Ajf.CoreSolver.Models;
using NUnit.Framework;
using RestSharp;

namespace Ajf.CoreSolver.Tests.Integration
{
    [TestFixture]
    public class IntegrationTest:BaseIntegrationTest
    {
        [Test]
        [Category("Integration")]
        public void ThatApiCanBeCalledLocally()
        {
            // Arrange
            var client = GetRestClient();
            var request = new RestRequest("api/calculation", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddBody(new CalculationRequest()); 

            // Act
            var response = client.Execute<CalculationResponse>(request);
            var content = response.Content; // raw content as string

            // Assert
        }
    }
}