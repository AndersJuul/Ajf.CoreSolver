using System.Linq;
using Ajf.CoreSolver.Models.External;
using Ajf.CoreSolver.Shared;
using Ajf.CoreSolver.Shared.Validation;
using Ajf.CoreSolver.SharedTests;
using Ajf.CoreSolver.Tests.Base;
using AutoFixture;
using NUnit.Framework;

namespace Ajf.CoreSolver.Tests.Shared
{
    [TestFixture]
    public class CalculationRequestValidatorTest : BaseUnitTest
    {
        [Test]
        public void ThatValidRequestIsValidated()
        {
            // Arrange
            var sut = new CalculationRequestValidator();
            var calculationRequest = TestDataProvider.GetValidCalculationRequest();

            // Act
            var validationResult = sut.Validate(calculationRequest);

            // Assert
            Assert.IsTrue(validationResult.IsValid, validationResult.ToString());
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
            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.ValidationItems.Any(x => x.Comment == "Unit must be supplied."));
        }
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
            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.ValidationItems.Any(x => x.Comment == "Desired algorithm for calculation must be supplied."));
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
            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.ValidationItems.Any(x => x.Comment == "Desired algorithm for calculation must be supplied."));
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
            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.ValidationItems.Any(x => x.Comment == "Desired algorithm for calculation does not match existing."));
        }
    }
}