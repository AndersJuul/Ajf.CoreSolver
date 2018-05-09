using System;
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
    public class IntegrationTest3 : BaseIntegrationTest
    {
        [Test]
        [Category("Integration")]
        [Timeout(20000)]
        public void ThatPostingValidCalculationIsSuccesful()
        {
            // Arrange
            var mapper = MapperProvider.GetMapper();
            var dbContextProvider = new DbContextProviderForTest(ConfigurationManager
                .ConnectionStrings["CoreSolverConnectionExpress"].ConnectionString);
            var calculationRepository = new CalculationRepository(dbContextProvider, mapper);

            calculationRepository.GetCalculationStatus(Guid.Parse(""));

                Log.Logger.Debug("Stage 7 ");
        }
    }
}