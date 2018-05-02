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
            var calculation = Fixture.Create<Calculation>();
            var calculationRequest = Fixture.Create<CalculationRequest>();
            var calculationRequestValidator = Fixture.Create<ICalculationRequestValidator>();
            var calculationRepository = Fixture.Create<ICalculationRepository>();
            var transactionId = Fixture.Create<Guid>();

            var validationResult = Fixture
                .Build<ValidationResult>()
                .With(x => x.IsValid, true)
                .Create();

            calculationRequestValidator
                .Stub(x => x.Validate(calculationRequest))
                .Return(validationResult);
            calculationRepository.Stub(x => x.InsertCalculation(calculation));

            var controller = new CalculationStatusController(calculationRequestValidator, calculationRepository, MapperProvider.GetMapper());

            // Act
            var result = controller.Get(transactionId);

            // Assert
            Assert.IsTrue(result is OkNegotiatedContentResult<CalculationResponse>, result.ToString());
            Assert.AreEqual(transactionId, (result as OkNegotiatedContentResult<CalculationResponse>).Content.TransactionId);
        }
    }
}