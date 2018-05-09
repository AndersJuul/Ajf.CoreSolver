using System.Configuration;
using System.Net;
using System.Threading;
using Ajf.CoreSolver.IntegrationTests.Base;
using Ajf.CoreSolver.Models.External;
using Ajf.CoreSolver.Shared;
using Ajf.CoreSolver.Shared.Service;
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
        [Timeout(30000)]
        public void ThatPostingValidCalculationIsSuccesful()
        {
            //TODO TEMP TEMP TEMP TEMP TEMP TEMP TEMP TEMP TEMP TEMP 
            // Run only locally; db issues on build.
            if (System.Environment.MachineName != "ANDERSJUULPC")
                return;
            //TODO TEMP TEMP TEMP TEMP TEMP TEMP TEMP TEMP TEMP TEMP 

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
            var appSettings = new AppSettings();
            using (var busAdapter = new BusAdapter(appSettings))
            {
                var connectionString = ConfigurationManager.ConnectionStrings["CoreSolverConnectionExpress"]
                    .ConnectionString;

                var dbContextProvider = new DbContextProviderForTest(connectionString);
                var mapper = MapperProvider.GetMapper();
                var calculationRepository = new CalculationRepository(dbContextProvider, mapper);
                var handleCalculationRequested =
                    new HandleCalculationRequested(busAdapter, appSettings, calculationRepository);
                using (var worker = new Worker(busAdapter,
                    handleCalculationRequested))
                {
                    //if (GetEnv() == Environment.LocalDev)
                        worker.Start();

                    // Assert
                    // Try every second, at most five times

                    CalculationResponse response;
                    do
                    {
                        var responseCheck = client.Execute<CalculationResponse>(requestGet);
                        Assert.AreEqual(HttpStatusCode.OK, responseCheck.StatusCode,
                            "Success was expected, got: " + responseCheck);
                        response = responseCheck.Data;
                        Thread.Sleep(1000);
                    } while (response.CalculationStatus != CalculationStatus.DoneAndSuccess);
                }
            }
        }
    }
}