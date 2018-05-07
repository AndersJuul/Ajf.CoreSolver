using Ajf.Nuget.Logging;

namespace Ajf.CoreSolver.WebApi
{
    public class AppSettings : WebSettingsFromConfigFile, IAppSettings
    {
        public AppSettings()
        {
            ExchangeName = $"{Environment}.{SuiteName}.CalcRequest";
        }

        public string ExchangeName { get; set; }
    }
}