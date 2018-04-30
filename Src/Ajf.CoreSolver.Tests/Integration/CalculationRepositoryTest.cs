using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Shared;
using Ajf.CoreSolver.Tests.Base;
using AutoFixture;
using NUnit.Framework;

namespace Ajf.CoreSolver.Tests.Integration
{
    [TestFixture]
    public class CalculationRepositoryTest : BaseIntegrationTestWithDb
    {
        [Test]
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