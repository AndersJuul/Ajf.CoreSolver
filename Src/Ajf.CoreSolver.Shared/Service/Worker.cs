using System;
using Ajf.CoreSolver.Shared.QueueEvents;
using Ajf.Nuget.TopShelf;
using EasyNetQ;
using Serilog;

namespace Ajf.CoreSolver.Shared.Service
{
    public class Worker : BaseWorker,IDisposable
    {
        private readonly IHandleCalculationRequested _handleCalculationRequested;
        private readonly IBusAdapter _bus;
        private ISubscriptionResult _subscriptionResult;

        public Worker(IBusAdapter bus, IHandleCalculationRequested handleCalculationRequested)
        {
            _bus = bus;
            _handleCalculationRequested = handleCalculationRequested;
        }

        public override void Start()
        {
            try
            {
                _subscriptionResult = _bus.Bus.SubscribeAsync<CalculationRequestedEvent>(
                    "CalculationRequestedEvent",
                    _handleCalculationRequested.Handle);

                Log.Logger.Information("Done starting consumers");
           }
            catch (Exception ex)
            {
                Log.Error(ex, "During Start", new object[0]);
                throw;
            }
        }


        public override void Stop()
        {
            if (_subscriptionResult != null)
            {
                _subscriptionResult.Dispose();
                _subscriptionResult = null;
            }
        }

        public void Dispose()
        {
            _subscriptionResult?.Dispose();
        }
    }
}