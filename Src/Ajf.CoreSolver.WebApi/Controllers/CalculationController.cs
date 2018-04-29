using System;
using System.Net;
using System.Web.Http;
using Ajf.CoreSolver.Models;
using Serilog;
using Serilog.Context;

namespace Ajf.CoreSolver.WebApi.Controllers
{
    /// <summary>
    ///     This is!
    /// </summary>
    public class CalculationController : ApiController
    {
        /// <summary>
        ///     Receives a calculationRequest and puts it in queue to be calculated.
        /// </summary>
        /// <param name="calculationRequest">The calculation to be made</param>
        /// <returns></returns>
        /// <response code="200">Returned with </response>
        /// <response code="400">Returned for technical (unanticipated) errors.</response>
        public IHttpActionResult Post([FromBody] CalculationRequest calculationRequest)
        {
            // transaction id to trace this calculation across processes.
            var transactionId = Guid.NewGuid();

            using (LogContext.PushProperty("TransactionId", transactionId))
            {
                try
                {
                    Log.Logger.Debug("CalculationRequest : {@CalculationRequest}", calculationRequest);

                    var calculationResponse = new CalculationResponse
                    {
                        TransactionId = transactionId
                    };

                    Log.Logger.Debug("Returning : {@CalculationResponse}", calculationResponse);

                    return Ok(calculationResponse);
                }
                catch (Exception e)
                {
                    Log.Logger.Error(e, "Calculation.Post");
                    return BadRequest(transactionId.ToString());
                }
            }
        }
    }
}