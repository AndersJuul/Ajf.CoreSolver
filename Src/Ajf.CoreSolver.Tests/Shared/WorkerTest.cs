using Ajf.CoreSolver.Models.External;
using Ajf.CoreSolver.Shared;
using Ajf.CoreSolver.Shared.QueueEvents;
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
    public class WorkerTest : BaseUnitTest
    {
        [Test]
        public void ThatWorkerCanBeStartedAndSubscriptionsMade()
        {
            // Arrange
            var bus = Fixture.Create<IBus>();
            var busAdapter = Fixture.Create<IBusAdapter>();
            busAdapter.Stub(x => x.Bus).Return(bus);
            var handleCalculationRequested = Fixture.Create<IHandleCalculationRequested>();
            bus.Expect(x => x.SubscribeAsync<CalculationRequestedEvent>("CalculationRequestedEvent",
                handleCalculationRequested.Handle)).Return(Fixture.Create<ISubscriptionResult>());

            var sut = new Worker(busAdapter, handleCalculationRequested);

            // Act
            sut.Start();

            // Assert
            bus.VerifyAllExpectations();
            busAdapter.VerifyAllExpectations();
        }
    }
}