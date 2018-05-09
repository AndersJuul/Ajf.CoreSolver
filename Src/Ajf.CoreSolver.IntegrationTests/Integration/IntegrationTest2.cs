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

namespace Ajf.CoreSolver.IntegrationTests.Integration
{
    [TestFixture]
    public class IntegrationTest2 : BaseIntegrationTest
    {
        [Test]
        [Category("Integration")]
        [Timeout(60000)]
        public void ThatPostingValidCalculationIsSuccesful()
        {
            // Arrange
            var calculationRequestValidator = new CalculationRequestValidator();
            var mapper = MapperProvider.GetMapper();
            var dbContextProvider = new DbContextProviderForTest(ConfigurationManager
                .ConnectionStrings["CoreSolverConnectionExpress"].ConnectionString);
            var calculationRepository = new CalculationRepository(dbContextProvider, mapper);
            var appSettings = new AppSettings();
            using (var busAdapter = new BusAdapter(appSettings))
            {
                var bus = busAdapter.Bus;

                var calculationController =
                    new CalculationController(calculationRequestValidator, calculationRepository, mapper, bus);

                var calculationStatusController = new CalculationStatusController(calculationRepository);

                var handleCalculationRequested =
                    new HandleCalculationRequested(busAdapter, appSettings, calculationRepository);
                var subscriptionResult = bus.SubscribeAsync<CalculationRequestedEvent>(
                    "CalculationRequestedEvent",
                    handleCalculationRequested.Handle);

                var httpActionResult = calculationController.Post(TestDataProvider.GetValidCalculationRequest());

                var okNegotiatedContentResult = httpActionResult as OkNegotiatedContentResult<CalculationResponse>;
                Assert.IsNotNull(okNegotiatedContentResult);
                Assert.IsNotNull(okNegotiatedContentResult.Content);
                do
                {
                    var actionResult = calculationStatusController.Get(okNegotiatedContentResult.Content.TransactionId);
                    var okNegotiatedContentResult1 = actionResult as OkNegotiatedContentResult<CalculationResponse>;
                    Assert.IsNotNull(okNegotiatedContentResult1);
                    Assert.IsNotNull(okNegotiatedContentResult1.Content);

                    if (okNegotiatedContentResult1.Content.CalculationStatus == CalculationStatus.DoneAndSuccess)
                        break;

                    Thread.Sleep(1000);
                } while (true);

                //subscriptionResult.Dispose();
            }
        }
    }
}