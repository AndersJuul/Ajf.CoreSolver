using System;
using System.Threading.Tasks;
using Ajf.CoreSolver.Shared;
using Ajf.CoreSolver.Shared.QueueEvents;
using Ajf.CoreSolver.Shared.Service;
using Ajf.CoreSolver.Tests.Base;
using AutoFixture;
using EasyNetQ;
using NUnit.Framework;
using Rhino.Mocks;

namespace Ajf.CoreSolver.Tests.Shared
{
    [TestFixture]
    public class HandleCalculationRequestedTest : BaseUnitTest
    {
        [Test]
        public async Task ThatWorkerCanBeStartedAndSubscriptionsMade()
        {
            // Arrange
            var appSettings = Fixture.Create<IAppSettings>();
            var bus = Fixture.Create<IBus>();
            var busAdapter = Fixture.Create<IBusAdapter>();
            busAdapter.Stub(x => x.Bus).Return(bus);
            //var handleCalculationRequested = Fixture.Create<IHandleCalculationRequested>();
            //bus.Expect(x => x.SubscribeAsync<CalculationRequestedEvent>("CalculationRequestedEvent",
            //    handleCalculationRequested.Handle)).Return(Fixture.Create<ISubscriptionResult>());
            var calculationRepository = Fixture.Create<ICalculationRepository>();
            var calculationRequestedEvent = Fixture.Build<CalculationRequestedEvent>().With(x => x.TransactionId, Fixture.Create<Guid>()).Create();

            var sut = new HandleCalculationRequested(busAdapter, appSettings,calculationRepository);

            // Act
            await sut.Handle(calculationRequestedEvent);

            // Assert
            appSettings.VerifyAllExpectations();
            calculationRepository.VerifyAllExpectations();
            bus.VerifyAllExpectations();
            busAdapter.VerifyAllExpectations();
        }
    }
}