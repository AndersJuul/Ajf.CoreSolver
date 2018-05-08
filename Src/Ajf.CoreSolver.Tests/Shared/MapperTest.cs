using Ajf.CoreSolver.DbModels;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Models.Internal;
using Ajf.CoreSolver.Shared;
using Ajf.CoreSolver.Tests.Base;
using Ajf.CoreSolver.WebApi.DependencyResolution;
using AutoFixture;
using NUnit.Framework;

namespace Ajf.CoreSolver.Tests.Shared
{
    [TestFixture]
    public class MapperTest : BaseUnitTest
    {
        [Test]
        public void ThatCalculationIsMapped()
        {
            // Arrange
            var sut = MapperProvider.GetMapper();

            var calculationEntity = Fixture
                .Build<CalculationEntity>()
                .Create();

            // Act
            var calculation = sut.Map<CalculationEntity,Calculation >(calculationEntity);

            // Assert
            Assert.AreEqual(calculationEntity.TransactionId, calculation.TransactionId);
            Assert.AreEqual(calculationEntity.CalculationStatus.ToString(), calculation.CalculationStatus.ToString());
        }
    }
}