//using System.Net;
//using Ajf.CoreSolver.Models;
//using Ajf.CoreSolver.Tests.Base;
//using NUnit.Framework;
//using RestSharp;
//using AutoFixture;

//namespace Ajf.CoreSolver.Tests.Integration
//{
//    [TestFixture]
//    public class IntegrationTest:BaseIntegrationTest
//    {
//        [Test]
//        [Category("Integration")]
//        public void ThatPostingValidCalculationIsSucces()
//        {
//            // Arrange
//            var client = GetRestClient();
//            var request = new RestRequest("api/calculation", Method.POST)
//            {
//                RequestFormat = DataFormat.Json
//            };
//            request.AddBody(Fixture.Build< CalculationRequest>().Create()); 

//            // Act
//            var response = client.Execute<CalculationResponse>(request);
            
//            // Assert
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//        }
//    }
//}