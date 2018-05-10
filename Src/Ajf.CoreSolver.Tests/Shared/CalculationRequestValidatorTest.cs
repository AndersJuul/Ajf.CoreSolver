using System;
using System.Linq;
using Ajf.CoreSolver.Shared.Validation;
using Ajf.CoreSolver.SharedTests;
using Ajf.CoreSolver.Tests.Base;
using AutoFixture;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Ajf.CoreSolver.Tests.Shared
{
    [TestFixture]
    public class CalculationRequestValidatorTest : BaseUnitTest
    {
        [Test]
        public void ThatRequestWithEmptyAlgorithmIsInvalid()
        {
            // Arrange
            var sut = new CalculationRequestValidator();
            var calculationRequest = TestDataProvider.GetValidCalculationRequest();
            calculationRequest.AlgorithmSelector = string.Empty;

            // Act
            var validationResult = sut.Validate(calculationRequest);

            // Assert
            Assert.IsFalse(validationResult.IsValid, JsonConvert.SerializeObject(validationResult));
            Assert.IsTrue(
                validationResult.ValidationItems.Any(x =>
                    x.Comment == "Desired algorithm for calculation must be supplied."),
                JsonConvert.SerializeObject(validationResult));
        }

        [Test]
        public void ThatRequestWithInvalidAlgorithmIsInvalid()
        {
            // Arrange
            var sut = new CalculationRequestValidator();
            var calculationRequest = TestDataProvider.GetValidCalculationRequest();
            calculationRequest.AlgorithmSelector = Fixture.Create<string>();

            // Act
            var validationResult = sut.Validate(calculationRequest);

            // Assert
            Assert.IsFalse(validationResult.IsValid, JsonConvert.SerializeObject(validationResult));
            Assert.IsTrue(
                validationResult.ValidationItems.Any(x =>
                    x.Comment == "Desired algorithm for calculation does not match existing."),
                JsonConvert.SerializeObject(validationResult));
        }

        [Test]
        public void ThatRequestWithNullAlgorithmIsInvalid()
        {
            // Arrange
            var sut = new CalculationRequestValidator();
            var calculationRequest = TestDataProvider.GetValidCalculationRequest();
            calculationRequest.AlgorithmSelector = null;

            // Act
            var validationResult = sut.Validate(calculationRequest);

            // Assert
            Assert.IsFalse(validationResult.IsValid, JsonConvert.SerializeObject(validationResult));
            Assert.IsTrue(
                validationResult.ValidationItems.Any(x =>
                    x.Comment == "Desired algorithm for calculation must be supplied."),
                JsonConvert.SerializeObject(validationResult));
        }

        [Test]
        public void ThatRequestWithoutUnitIsInvalid()
        {
            // Arrange
            var sut = new CalculationRequestValidator();
            var calculationRequest = TestDataProvider.GetValidCalculationRequest();
            calculationRequest.Unit = null;

            // Act
            var validationResult = sut.Validate(calculationRequest);

            // Assert
            Assert.IsFalse(validationResult.IsValid, JsonConvert.SerializeObject(validationResult));
            Assert.IsTrue(validationResult.ValidationItems.Any(x => x.Comment == "Root Unit must be supplied."),
                JsonConvert.SerializeObject(validationResult));
        }

        [Test]
        public void ThatRequestWithUnitThatHasNoIdIsInvalid()
        {
            // Arrange
            var sut = new CalculationRequestValidator();
            var calculationRequest = TestDataProvider.GetValidCalculationRequest();
            calculationRequest.Unit.Id = Guid.Empty;

            // Act
            var validationResult = sut.Validate(calculationRequest);

            // Assert
            Assert.IsFalse(validationResult.IsValid, JsonConvert.SerializeObject(validationResult));
            Assert.IsTrue(
                validationResult.ValidationItems.Any(x =>
                    x.Comment.StartsWith( "Unit with empty ID found at ")),
                JsonConvert.SerializeObject(validationResult));
        }

        [Test]
        public void ThatValidRequestIsValidated()
        {
            // Arrange
            var sut = new CalculationRequestValidator();
            var calculationRequest = TestDataProvider.GetValidCalculationRequest();

            // Act
            var validationResult = sut.Validate(calculationRequest);

            // Assert
            Assert.IsTrue(validationResult.IsValid, JsonConvert.SerializeObject(validationResult));
        }
    }
}