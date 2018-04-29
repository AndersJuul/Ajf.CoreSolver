using System;
using System.Web.Http;
using Ajf.CoreSolver.Models;
using Serilog;
using Serilog.Context;

namespace Ajf.CoreSolver.WebApi.Controllers
{
    /// <summary>
    /// This is!
    /// </summary>
    public class CalculationController : ApiController
    {
        /// <summary>
        /// And this is too!
        /// </summary>
        /// <param name="calculationRequest"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] CalculationRequest calculationRequest)
        {
            var transactionId = Guid.NewGuid();
            using (LogContext.PushProperty("TransactionId", transactionId))
            {
                Log.Logger.Debug("CalculationRequest : {@CalculationRequest}", calculationRequest);

                var calculationResponse = new CalculationResponse
                {
                    TransactionId = transactionId
                };

                Log.Logger.Debug("Returning : {@CalculationResponse}", calculationResponse);

                return Ok(calculationResponse);
            }
        }
    }
}