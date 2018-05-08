using System;
using System.Reflection;
using System.Threading.Tasks;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Shared.QueueEvents;
using Serilog;
using Serilog.Context;

namespace Ajf.CoreSolver.Shared.Service
{
    public class HandleCalculationRequested : IHandleCalculationRequested
    {
        private readonly IAppSettings _appSettings;
        private readonly ICalculationRepository _calculationRepository;
        private readonly IBusAdapter _bus;

        public HandleCalculationRequested(IBusAdapter bus, IAppSettings appSettings, ICalculationRepository calculationRepository)
        {
            _appSettings = appSettings;
            _calculationRepository = calculationRepository;
            _bus = bus;
        }

        public async Task Handle(CalculationRequestedEvent message)
        {
            using (LogContext.PushProperty("Method", MethodBase.GetCurrentMethod().Name))
            using (LogContext.PushProperty("TransactionId", message.TransactionId))
            {
                try
                {
                    Log.Logger.Information("Message received : {@message}", message);

                    _calculationRepository.SetCalculationStatus(message.TransactionId,
                        CalculationStatus.DoneAndSuccess);
                    Log.Logger.Information("Done updating db");

                    await Task.FromResult(0);
                }
                catch (Exception e)
                {
                    Log.Logger.Error(e, "Exception during processing");
                    throw;
                }
            }
        }
    }
}