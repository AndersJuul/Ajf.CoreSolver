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
    public class CalculationController : ApiController
    {
        private readonly ICalculationRequestValidator _calculationRequestValidator;
        private readonly ICalculationRepository _calculationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Entry for new calculations and getting status on calculations requested
        /// </summary>
        /// <param name="calculationRequestValidator"></param>
        /// <param name="calculationRepository"></param>
        public CalculationController(ICalculationRequestValidator calculationRequestValidator,
            ICalculationRepository calculationRepository, IMapper mapper)
        {
            _calculationRequestValidator = calculationRequestValidator;
            _calculationRepository = calculationRepository;
            _mapper = mapper;
        }

        /// <summary>
        ///     Receives a calculationRequest and puts it in queue to be calculated.
        /// </summary>
        /// <param name="calculationRequest">The calculation to be made</param>
        /// <returns></returns>
        /// <response code="200">Returned with CalculationResponse for success</response>
        /// <response code="400">Returned for technical (unanticipated) errors.</response>
        /// <response code="400">Returned for validation errors; validation feedback in response (for debug).</response>
        public IHttpActionResult Post([FromBody] CalculationRequest calculationRequest)
        {
            // transaction id to trace this calculation across processes.
            var transactionId = Guid.NewGuid();

            using (LogContext.PushProperty("TransactionId", transactionId))
            {
                try
                {
                    Log.Logger.Debug("CalculationRequest : {@CalculationRequest}", calculationRequest);

                    // ------------
                    // Validate input, return description of the problem if failing.
                    var validationResult =_calculationRequestValidator.Validate(calculationRequest);
                    if (!validationResult.IsValid)
                    {
                        return Content(HttpStatusCode.BadRequest, validationResult.ToString());
                    }

                    // ------------
                    // Convert the parameter to internal model                    
                    var calculation = _mapper.Map<CalculationRequest, Calculation>(calculationRequest);
                    calculation.TransactionId = transactionId;

                    // ------------
                    // Insert the request in database to keep track of calculations
                    // (Will throw ex if the transactionId has been used already)
                    _calculationRepository.InsertCalculation(calculation);

                    // ------------
                    // Add request to queue and let the queue processor handle it.
                    // ...

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