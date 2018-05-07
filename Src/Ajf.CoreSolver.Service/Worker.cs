using System;
using System.ComponentModel;
using Ajf.CoreSolver.Shared.QueueEvents;
using Ajf.Nuget.TopShelf;
using Serilog;

namespace Ajf.CoreSolver.Service
{
    public class Worker : BaseWorker
    {
        private readonly IAppSettings _appSettings;
        private readonly IHandleCalculationRequested _handleCalculationRequested;
        private readonly IBusAdapter _bus;

        public Worker(IBusAdapter bus, IAppSettings appSettings, IHandleCalculationRequested handleCalculationRequested)
        {
            _bus = bus;
            _appSettings = appSettings;
            _handleCalculationRequested = handleCalculationRequested;
        }

        public override void Start()
        {
            try
            {
                var backgroundWorkerSetup = new BackgroundWorker();
                backgroundWorkerSetup.DoWork += BackgroundWorker_DoWork;
                backgroundWorkerSetup.RunWorkerAsync();
           }
            catch (Exception ex)
            {
                Log.Error(ex, "During Start", new object[0]);
                throw;
            }
        }


        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _bus.Bus.SubscribeAsync<CalculationRequestedEvent>(
                "CalculationRequestedEvent",
                _handleCalculationRequested.Handle);

            Log.Logger.Information("Done starting consumers");
        }

        public override void Stop()
        {
        }
    }
}