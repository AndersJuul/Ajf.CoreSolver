using Ajf.CoreSolver.IntegrationTests.Base;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Shared;
using AutoFixture;
using NUnit.Framework;

namespace Ajf.CoreSolver.IntegrationTests.Integration
{
    [TestFixture]
    public class CalculationRepositoryTest : BaseIntegrationTestWithDb
    {
        [Test]
        [Category("Integration")]
        public void ThatCalculationCanBeInsertedAndRetrieved()
        {
            // Arrange
            var dbContextProviderForTest = new DbContextProviderForTest(ConnectionString);
            var sut = new CalculationRepository(dbContextProviderForTest);
            var calculationRequest = Fixture
                .Build<CalculationRequest>()
                .Create();

            // Act
            sut.InsertCalculation(calculationRequest);

            // Assert
            //Assert.IsTrue(validationResult.IsValid);
        }
    }
}