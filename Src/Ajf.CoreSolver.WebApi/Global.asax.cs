using Ajf.Nuget.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ajf.CoreSolver.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Log.Logger = StandardLoggerConfigurator
    .GetLoggerConfig().MinimumLevel
    .Debug()
    .CreateLogger();

            Log.Logger.Information("Starting...");
            Log.Logger.Information("Version is... " + ConfigurationManager.AppSettings["ReleaseNumber"]);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
