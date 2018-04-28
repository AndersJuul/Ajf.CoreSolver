using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Ajf.CoreSolver.Models;

namespace Ajf.CoreSolver.WebApi.Controllers
{
    public class CalculationController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            Log.Logger.Debug("Get() called");

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            Log.Logger.Debug("Get(int id) called");

            return "value";
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]CalculationRequest calculationRequest)
        {
            Log.Logger.Debug("CalculationRequest : {@CalculationRequest}", calculationRequest);

            return Ok(new CalculationResponse());
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
