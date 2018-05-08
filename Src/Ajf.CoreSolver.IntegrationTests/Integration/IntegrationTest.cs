using System.Net;
using Ajf.CoreSolver.IntegrationTests.Base;
using Ajf.CoreSolver.Models.External;
using Ajf.CoreSolver.SharedTests;
using NUnit.Framework;
using RestSharp;

namespace Ajf.CoreSolver.IntegrationTests.Integration
{
    [TestFixture]
    public class IntegrationTest : BaseIntegrationTest
    {
        [Test]
        [Category("Integration")]
        [Timeout(40000)]
        public void ThatPostingValidCalculationIsSuccesful()
        {
            // Arrange
            var client = GetRestClient();
            var requestPost = new RestRequest("api/calculation", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            requestPost.AddBody(TestDataProvider.GetValidCalculationRequest());

            // Act
            var responsePost = client.Execute<CalculationResponse>(requestPost);

            var requestGet = new RestRequest("api/calculationstatus", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            requestGet.Parameters.Add(new Parameter
            {
                Name = "TransactionId",
                Value = responsePost.Data.TransactionId,
                Type = ParameterType.QueryString
            });

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, responsePost.StatusCode, "Success was expected, got: " + responsePost);

            // On local dev we need to spin up a worker as it would have been done from service.
            // Service is NOT running on local dev macines during the running of integration tests.
            // Note: It is running until disposed, hence we keep the reference though
            // apparently not used.
            // ReSharper disable once NotAccessedVariable
            //var appSettings = new AppSettings();
            //var bus = new BusAdapter(appSettings);

            //using (var worker = new Worker(bus,
            //    new HandleCalculationRequested(bus, appSettings,
            //        new CalculationRepository(new DbContextProvider(), MapperProvider.GetMapper()))))
            //{
            //    if (GetEnv() == Environment.LocalDev) worker.Start();

            //    // Assert
            //    // Try every second, at most five times

            //    CalculationResponse response;
            //    do
            //    {
            //        var responseCheck = client.Execute<CalculationResponse>(requestGet);
            //        Assert.AreEqual(HttpStatusCode.OK, responseCheck.StatusCode,
            //            "Success was expected, got: " + responseCheck);
            //        response = responseCheck.Data;
            //        Thread.Sleep(1000);
            //    } while (response.CalculationStatus!=CalculationStatus.DoneAndSuccess);
            //}
        }
    }
}