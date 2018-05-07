using System;
using System.Threading.Tasks;
using Ajf.CoreSolver.Shared.QueueEvents;
using Serilog;

namespace Ajf.CoreSolver.Service
{
    public class HandleCalculationRequested : IHandleCalculationRequested
    {
        private readonly IAppSettings _appSettings;
        private readonly IBusAdapter _bus;

        public HandleCalculationRequested(IBusAdapter bus, IAppSettings appSettings)
        {
            _appSettings = appSettings;
            _bus = bus;
        }

        public async Task Handle(CalculationRequestedEvent message)
        {
            try
            {
                Log.Logger.Information("Message received : {@message}", message);

                //await _bus.Bus.PublishAsync(Mapper.Map<FileProcessedEvent>(message))
                //    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Exception during processing");
                throw;
            }
        }
    }
}