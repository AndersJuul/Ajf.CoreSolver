using System;
using RestSharp;

namespace Ajf.CoreSolver.Tests.Base
{
    public abstract class BaseIntegrationTest:BaseTest
    {
        private string getTarget()
        {
            if (Environment.MachineName.ToLower() == "ajf-build-01")
                return "ajf-qa-02";

            return "localhost";
        }

        protected RestClient GetRestClient()
        {
            var target = getTarget();
            var client = new RestClient("http://" + target + "/Ajf.CoreSolver.WebApi/");
            return client;
        }
    }
}