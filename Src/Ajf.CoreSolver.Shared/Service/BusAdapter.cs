using EasyNetQ;
using Serilog;

namespace Ajf.CoreSolver.Shared.Service
{
    public class BusAdapter : IBusAdapter
    {
        private readonly IAppSettings _appSettings;

        public BusAdapter(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        private IBus _bus;

        // 
        public IBus Bus
        {
            get
            {
                if (_bus == null)
                {
                    Log.Logger.Information("Creating Bus connection.");
                    _bus = RabbitHutch.CreateBus(_appSettings.EasyNetQConfig);
                    Log.Logger.Information("Done creating Bus connection.");
                }
                return _bus;
            }
        }
    }
}