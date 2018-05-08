using Ajf.CoreSolver.Models.External;
using Ajf.CoreSolver.Shared;
using Ajf.CoreSolver.Shared.Service;
using Ajf.CoreSolver.Shared.Validation;
using Ajf.CoreSolver.SharedTests;
using Ajf.CoreSolver.Tests.Base;
using AutoFixture;
using EasyNetQ;
using NUnit.Framework;
using Rhino.Mocks;

namespace Ajf.CoreSolver.Tests.Shared
{
    [TestFixture]
    public class BusAdapterTest : BaseUnitTest
    {
        [Test]
        public void ThatBusCanBeProvidedWithConnectionStringFromSettings()
        {
            // Arrange
            var appSettings = Fixture.Create<IAppSettings>();
            appSettings.Stub(x => x.EasyNetQConfig).Return("host=andersathome.dk:9600;virtualHost=dev;username=none;password=none;timeout=0;requestedHeartbeat=0");
            var sut = new BusAdapter(appSettings);

            // Act
            var bus = sut.Bus;

            // Assert
            Assert.IsNotNull(bus);
            appSettings.VerifyAllExpectations();
        }
    }
}