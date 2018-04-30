using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Shared;
using Ajf.CoreSolver.Tests.Base;
using AutoFixture;
using NUnit.Framework;

namespace Ajf.CoreSolver.Tests.Shared
{
    [TestFixture]
    public class CalculationRequestValidatorTest : BaseUnitTest
    {
        [Test]
        public void Index()
        {
            // Arrange
            var sut = new CalculationRequestValidator();
            var calculationRequest = Fixture
                .Build<CalculationRequest>()
                .Create();

            // Act
            var validationResult = sut.Validate(calculationRequest);

            // Assert
            Assert.IsTrue(validationResult.IsValid);
        }
    }
}