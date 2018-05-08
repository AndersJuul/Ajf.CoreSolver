using System.Diagnostics.CodeAnalysis;
using Ajf.Nuget.Logging;

namespace Ajf.CoreSolver.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AppSettings : WebSettingsFromConfigFile, IAppSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public AppSettings()
        {
            ExchangeName = $"{Environment}.{SuiteName}.CalcRequest";
        }

        /// <summary>
        /// 
        /// </summary>
        public string ExchangeName { get; set; }
    }
}