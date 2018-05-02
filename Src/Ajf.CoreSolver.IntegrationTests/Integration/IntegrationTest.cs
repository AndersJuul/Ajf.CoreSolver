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
            var requestPost = new RestRequest("api/calculation", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            requestPost.AddBody(Fixture.Build<CalculationRequest>().Create());

            // Act
            var responsePost = client.Execute<CalculationResponse>(requestPost);

            var requestGet = new RestRequest("api/calculation", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            requestGet.Parameters.Add(new Parameter() { Name= "TransactionId",Value = responsePost.Data.TransactionId, Type = ParameterType.QueryString} ); 
            var responseCheck = client.Execute<CalculationResponse>(requestGet);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, responsePost.StatusCode, "Success was expected, got: " + responsePost);
            Assert.AreEqual(HttpStatusCode.OK, responseCheck.StatusCode, "Success was expected, got: " + responseCheck);
        }
    }
}