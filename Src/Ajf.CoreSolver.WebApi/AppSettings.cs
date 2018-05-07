using System.Diagnostics.CodeAnalysis;
using Ajf.Nuget.Logging;

namespace Ajf.CoreSolver.WebApi
{
    [ExcludeFromCodeCoverage]
    public class AppSettings : WebSettingsFromConfigFile, IAppSettings
    {
        public AppSettings()
        {
            ExchangeName = $"{Environment}.{SuiteName}.CalcRequest";
        }

        public string ExchangeName { get; set; }
    }
}