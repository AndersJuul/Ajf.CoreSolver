using System;
using System.Web.Http.Results;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Models.Internal;
using Ajf.CoreSolver.Shared;
using Ajf.CoreSolver.Tests.Base;
using Ajf.CoreSolver.WebApi.Controllers;
using Ajf.CoreSolver.WebApi.DependencyResolution;
using AutoFixture;
using NUnit.Framework;
using Rhino.Mocks;

namespace Ajf.CoreSolver.Tests.WebApi.Controllers
{
    [TestFixture]
    public class CalculationStatusControllerTest : BaseUnitTest
    {
        [Test]
        public void Get()
        {
            // Arrange
            var calculationRequestValidator = Fixture.Create<ICalculationRequestValidator>();
            var calculationRepository = Fixture.Create<ICalculationRepository>();
            var transactionId = Fixture.Create<Guid>();

            calculationRepository.Stub(x => x.GetCalculationStatus(transactionId)).Return(CalculationStatus.CalculationQueued);

            var controller = new CalculationStatusController(calculationRequestValidator, calculationRepository, MapperProvider.GetMapper());

            // Act
            var result = controller.Get(transactionId);

            // Assert
            Assert.IsTrue(result is OkNegotiatedContentResult<CalculationResponse>, result.ToString());
            var calculationResponse = (result as OkNegotiatedContentResult<CalculationResponse>).Content;
            Assert.AreEqual(transactionId, calculationResponse.TransactionId);
            Assert.AreEqual(CalculationStatus.CalculationQueued, calculationResponse.CalculationStatus);
        }
    }
}