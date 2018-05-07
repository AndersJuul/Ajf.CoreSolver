using Ajf.CoreSolver.IntegrationTests.Base;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Models.Internal;
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
            var sut = new CalculationRepository(dbContextProviderForTest, MapperProvider.GetMapper());
            var calculation = Fixture
                .Build<Calculation>()
                .With(x=>x.CalculationStatus,CalculationStatus.CalculationQueued)
                .Create();

            // Act
            sut.InsertCalculation(calculation);
            var retrieved = sut.GetCalculationStatus(calculation.TransactionId);

            // Assert
            Assert.AreEqual(calculation.CalculationStatus, retrieved);
        }
    }
}