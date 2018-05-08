using System;
using Ajf.CoreSolver.Tests.Base;
using Ajf.Nuget.Logging;
using NUnit.Framework;
using RestSharp;
using Serilog;

namespace Ajf.CoreSolver.IntegrationTests.Base
{
    public abstract class BaseIntegrationTest:BaseTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Log.Logger = StandardLoggerConfigurator
                .GetLoggerConfig().MinimumLevel
                .Debug()
                .CreateLogger();
        }

        public Environment GetEnv()
        {
            if (System.Environment.MachineName.ToLower() == "ajf-build-01")
                return Environment.QA;
            return Environment.LocalDev;
        }
        private string getTarget()
        {
            if (GetEnv()==Environment.QA)
                return "ajf-qa-02";

            return "localhost";
        }

        protected RestClient GetRestClient()
        {
            var target = getTarget();
            var client = new RestClient("http://" + target + "/Ajf.CoreSolver.WebApi/");
            return client;
        }

        public enum Environment
        {
            LocalDev,
            QA
        }
    }
}