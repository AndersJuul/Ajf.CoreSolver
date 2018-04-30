using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Ajf.Nuget.Logging;
using Serilog;

namespace Ajf.CoreSolver.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Log.Logger = StandardLoggerConfigurator
                .GetLoggerConfig().MinimumLevel
                .Debug()
                .CreateLogger();

            Log.Logger.Information("Starting WebApi - Version is... " + ConfigurationManager.AppSettings["ReleaseNumber"]);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}