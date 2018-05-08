using Ajf.CoreSolver.Models.External;
using Ajf.CoreSolver.Shared;
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
            var calculationRequest =TestDataProvider.GetValidCalculationRequest();

            // Act
            var validationResult = sut.Validate(calculationRequest);

            // Assert
            Assert.IsTrue(validationResult.IsValid);
        }
    }
}