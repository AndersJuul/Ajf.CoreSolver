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
        public IHttpActionResult Post([FromBody]CalculationRequest calculationRequest)
        {
            Log.Logger.Debug("CalculationRequest : {@CalculationRequest}", calculationRequest);

            var calculationResponse = new CalculationResponse();

            Log.Logger.Debug("Returning : {@CalculationResponse}", calculationResponse);

            return Ok(calculationResponse);
        }

    }
}
