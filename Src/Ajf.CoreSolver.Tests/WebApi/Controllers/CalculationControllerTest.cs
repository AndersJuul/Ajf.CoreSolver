using System;
using System.Web.Http.Results;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Models.External;
using Ajf.CoreSolver.Models.Internal;
using Ajf.CoreSolver.Shared;
using Ajf.CoreSolver.SharedTests;
using Ajf.CoreSolver.Tests.Base;
using Ajf.CoreSolver.Tests.Shared;
using Ajf.CoreSolver.WebApi.Controllers;
using AutoFixture;
using EasyNetQ;
using NUnit.Framework;
using Rhino.Mocks;

namespace Ajf.CoreSolver.Tests.WebApi.Controllers
{
    [TestFixture]
    public class CalculationControllerTest : BaseUnitTest
    {
        [Test]
        public void ThatPostingInvalidReturnsBadRequest()
        {
            // Arrange
            var calculationRequest =TestDataProvider.GetInvalidCalculationRequest();
            var calculationRequestValidator = Fixture.Create<ICalculationRequestValidator>();
            var calculationRepository = Fixture.Create<ICalculationRepository>();
            var bus = Fixture.Create<IBus>();

            var validationResult = Fixture
                .Build<ValidationResult>()
                .With(x => x.IsValid, false)
                .Create();

            calculationRequestValidator
                .Stub(x => x.Validate(calculationRequest))
                .Return(validationResult);

            var controller = new CalculationController(
                calculationRequestValidator,
                calculationRepository,
                MapperProvider.GetMapper(),
                bus);

            // Act
            var response = controller.Post(calculationRequest);

            // Assert
            Assert.IsTrue(response is BadRequestErrorMessageResult, response.ToString());
        }

        [Test]
        public void ThatPostingValidReturnsOk()
        {
            // Arrange
            var calculation = Fixture.Create<Calculation>();
            var calculationRequest =TestDataProvider. GetValidCalculationRequest();
            var calculationRequestValidator = Fixture.Create<ICalculationRequestValidator>();
            var calculationRepository = Fixture.Create<ICalculationRepository>();
            var bus = Fixture.Create<IBus>();

            var validationResult = Fixture
                .Build<ValidationResult>()
                .With(x => x.IsValid, true)
                .Create();

            calculationRequestValidator
                .Stub(x => x.Validate(calculationRequest))
                .Return(validationResult);
            calculationRepository.Stub(x => x.InsertCalculation(calculation));

            var controller = new CalculationController(
                calculationRequestValidator,
                calculationRepository,
                MapperProvider.GetMapper(),
                bus);

            // Act
            var response = controller.Post(calculationRequest);

            // Assert
            Assert.IsTrue(response is OkNegotiatedContentResult<CalculationResponse>, response.ToString());
        }

        [Test]
        public void ThatPostingWithResultingExceptionReturnsBadRequest()
        {
            // Arrange
            var calculationRequest =TestDataProvider.GetInvalidCalculationRequest();
            var calculationRequestValidator = Fixture.Create<ICalculationRequestValidator>();
            var calculationRepository = Fixture.Create<ICalculationRepository>();
            var bus = Fixture.Create<IBus>();

            calculationRequestValidator
                .Stub(x => x.Validate(calculationRequest))
                .Throw(new Exception());

            var controller = new CalculationController(
                calculationRequestValidator,
                calculationRepository,
                MapperProvider.GetMapper(),
                bus);

            // Act
            var response = controller.Post(calculationRequest);

            // Assert
            Assert.IsTrue(response is BadRequestErrorMessageResult, response.ToString());
        }
    }
}