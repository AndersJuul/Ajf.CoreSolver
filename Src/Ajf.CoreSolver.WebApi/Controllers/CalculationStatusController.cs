using System;
using System.Net;
using System.Web.Http;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Models.Internal;
using Ajf.CoreSolver.Shared;
using AutoMapper;
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
        private readonly ICalculationRequestValidator _calculationRequestValidator;
        private readonly IMapper _mapper;

        /// <summary>
        ///     Entry for new calculations and getting status on calculations requested
        /// </summary>
        /// <param name="calculationRequestValidator"></param>
        /// <param name="calculationRepository"></param>
        /// <param name="mapper"></param>
        public CalculationStatusController(ICalculationRequestValidator calculationRequestValidator,
            ICalculationRepository calculationRepository, IMapper mapper)
        {
            _calculationRequestValidator = calculationRequestValidator;
            _calculationRepository = calculationRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns the status of a calculation
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public IHttpActionResult Get(Guid transactionId)
        {
            using (LogContext.PushProperty("TransactionId", transactionId))
            {
                try
                {
                    // ------------
                    // Return a response indicating success and with transaction id
                    //   for when the caller wish to query results.
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