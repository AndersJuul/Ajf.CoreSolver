using System;
using System.Reflection;
using System.Web.Http;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Shared;
using Serilog;
using Serilog.Context;

namespace Ajf.CoreSolver.WebApi.Controllers
{
    /// <summary>
    ///     This is!
    /// </summary>
    public class CalculationStatusController : ApiController
    {
        private readonly ICalculationRepository _calculationRepository;

        /// <summary>
        ///     Entry for new calculations and getting status on calculations requested
        /// </summary>
        /// <param name="calculationRepository"></param>
        public CalculationStatusController(ICalculationRepository calculationRepository)
        {
            _calculationRepository = calculationRepository;
        }

        /// <summary>
        ///     Returns the status of a calculation
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public IHttpActionResult Get(Guid transactionId)
        {
            using (LogContext.PushProperty("Method", MethodBase.GetCurrentMethod().Name) )
            using (LogContext.PushProperty("TransactionId", transactionId))
            {
                try
                {
                    var calculationStatus = _calculationRepository.GetCalculationStatus(transactionId);

                    // ------------
                    // Return a response indicating success and with transaction id
                    //   for when the caller wish to query results.
                    var calculationResponse = new CalculationResponse
                    {
                        TransactionId = transactionId,
                        CalculationStatus = calculationStatus
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