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
        public void ThatValidRequestIsValidated()
        {
            // Arrange
            var sut = MapperProvider.GetMapper();

            var calculationEntity = Fixture
                .Build<CalculationEntity>()
                .Create();

            // Act
            var validationResult = sut.Map<CalculationEntity,Calculation >(calculationEntity);

            // Assert
            //Assert.IsTrue(validationResult.IsValid);
        }
    }
}