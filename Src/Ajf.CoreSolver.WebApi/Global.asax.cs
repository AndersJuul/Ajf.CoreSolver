﻿using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Ajf.Nuget.Logging;
using Serilog;

namespace Ajf.CoreSolver.WebApi
{
    [ExcludeFromCodeCoverage]
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Log.Logger = StandardLoggerConfigurator
                .GetLoggerConfig().MinimumLevel
                .Debug()
                .CreateLogger();

            Log.Logger.Information("Starting WebApi");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}