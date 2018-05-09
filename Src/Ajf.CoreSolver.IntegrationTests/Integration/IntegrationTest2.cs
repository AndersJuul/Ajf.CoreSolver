using System.Configuration;
using System.Net;
using System.Threading;
using System.Web.Http.Results;
using Ajf.CoreSolver.IntegrationTests.Base;
using Ajf.CoreSolver.Models.External;
using Ajf.CoreSolver.Shared;
using Ajf.CoreSolver.Shared.QueueEvents;
using Ajf.CoreSolver.Shared.Service;
using Ajf.CoreSolver.Shared.Validation;
using Ajf.CoreSolver.SharedTests;
using Ajf.CoreSolver.WebApi.Controllers;
using NUnit.Framework;
using RestSharp;
using Serilog;

namespace Ajf.CoreSolver.IntegrationTests.Integration
{
    [TestFixture]
    public class IntegrationTest2 : BaseIntegrationTest
    {
        [Test]
        [Category("Integration")]
        [Timeout(20000)]
        public void ThatPostingValidCalculationIsSuccesful()
        {
            //TODO TEMP TEMP TEMP TEMP TEMP TEMP TEMP TEMP TEMP TEMP 
            // Run only locally; db issues on build.
            if (System.Environment.MachineName != "ANDERSJUULPC")
                return;
            //TODO TEMP TEMP TEMP TEMP TEMP TEMP TEMP TEMP TEMP TEMP 

            // Arrange
            var calculationRequestValidator = new CalculationRequestValidator();
            var mapper = MapperProvider.GetMapper();
            var dbContextProvider = new DbContextProviderForTest(ConfigurationManager
                .ConnectionStrings["CoreSolverConnectionExpress"].ConnectionString);
            var calculationRepository = new CalculationRepository(dbContextProvider, mapper);
            var appSettings = new AppSettings();

            Log.Logger.Debug("Stage 1 ");

            using (var busAdapter = new BusAdapter(appSettings))
            {
                Log.Logger.Debug("Stage 2 ");
                var bus = busAdapter.Bus;

                Log.Logger.Debug("Stage 3 ");

                var calculationController =
                    new CalculationController(calculationRequestValidator, calculationRepository, mapper, bus);

                var calculationStatusController = new CalculationStatusController(calculationRepository);

                var handleCalculationRequested =
                    new HandleCalculationRequested(busAdapter, appSettings, calculationRepository);
                var subscriptionResult = bus.SubscribeAsync<CalculationRequestedEvent>(
                    "CalculationRequestedEvent",
                    handleCalculationRequested.Handle);

                Log.Logger.Debug("Stage 4 ");

                var httpActionResult = calculationController.Post(TestDataProvider.GetValidCalculationRequest());

                var okNegotiatedContentResult = httpActionResult as OkNegotiatedContentResult<CalculationResponse>;
                Assert.IsNotNull(okNegotiatedContentResult);
                Assert.IsNotNull(okNegotiatedContentResult.Content);
                do
                {
                    Log.Logger.Debug("Stage 5 ");

                    var actionResult = calculationStatusController.Get(okNegotiatedContentResult.Content.TransactionId);
                    var okNegotiatedContentResult1 = actionResult as OkNegotiatedContentResult<CalculationResponse>;
                    Assert.IsNotNull(okNegotiatedContentResult1);
                    Assert.IsNotNull(okNegotiatedContentResult1.Content);

                    if (okNegotiatedContentResult1.Content.CalculationStatus == CalculationStatus.DoneAndSuccess)
                        break;

                    Log.Logger.Debug("Stage 6 ");

                    Thread.Sleep(1000);
                } while (true);

                Log.Logger.Debug("Stage 7 ");

                //subscriptionResult.Dispose();
            }
        }
    }
}