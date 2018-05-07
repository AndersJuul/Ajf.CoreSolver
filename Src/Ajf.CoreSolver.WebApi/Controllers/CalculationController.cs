﻿using System;
using System.Net;
using System.Reflection;
using System.Web.Http;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Models.Internal;
using Ajf.CoreSolver.Shared;
using Ajf.CoreSolver.Shared.QueueEvents;
using AutoMapper;
using EasyNetQ;
using Serilog;
using Serilog.Context;

namespace Ajf.CoreSolver.WebApi.Controllers
{
    /// <summary>
    ///     This is!
    /// </summary>
    public class CalculationController : ApiController
    {
        private readonly ICalculationRepository _calculationRepository;
        private readonly ICalculationRequestValidator _calculationRequestValidator;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        /// <summary>
        ///     Entry for new calculations and getting status on calculations requested
        /// </summary>
        /// <param name="calculationRequestValidator"></param>
        /// <param name="calculationRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="bus"></param>
        public CalculationController(ICalculationRequestValidator calculationRequestValidator,
            ICalculationRepository calculationRepository, IMapper mapper, IBus bus)
        {
            _calculationRequestValidator = calculationRequestValidator;
            _calculationRepository = calculationRepository;
            _mapper = mapper;
            _bus = bus;
        }

        ///// <summary>
        ///// Returns the status of a calculation
        ///// </summary>
        ///// <param name="transactionId"></param>
        ///// <returns></returns>
        //public IHttpActionResult Get(Guid transactionId)
        //{
        //    using (LogContext.PushProperty("TransactionId", transactionId))
        //    {
        //        try
        //        {
        //            // ------------
        //            // Return a response indicating success and with transaction id
        //            //   for when the caller wish to query results.
        //            var calculationResponse = new CalculationResponse
        //            {
        //                TransactionId = transactionId
        //            };

        //            Log.Logger.Debug("Returning : {@CalculationResponse}", calculationResponse);

        //            return Ok(calculationResponse);
        //        }
        //        catch (Exception e)
        //        {
        //            Log.Logger.Error(e, "Calculation.Post");
        //            return BadRequest(transactionId.ToString());
        //        }
        //    }
        //}

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

            using (LogContext.PushProperty("Method", MethodBase.GetCurrentMethod().Name))
            using (LogContext.PushProperty("TransactionId", transactionId))
            {
                try
                {
                    Log.Logger.Debug("CalculationRequest : {@CalculationRequest}", calculationRequest);

                    // ------------
                    // Validate input, return description of the problem if failing.
                    var validationResult = _calculationRequestValidator.Validate(calculationRequest);
                    if (!validationResult.IsValid)
                    {
                        Log.Logger.Debug("Invalid request : {@ValidationResult}", validationResult);
                        return BadRequest(validationResult.ToString());
                    }

                    // ------------
                    // Convert the request to internal model, add transaction id and status. 
                    var calculation = _mapper.Map<CalculationRequest, Calculation>(calculationRequest);
                    calculation.TransactionId = transactionId;
                    calculation.CalculationStatus = CalculationStatus.CalculationQueued;

                    // ------------
                    // Insert the request in database to keep track of calculations
                    // (Will throw ex if the transactionId has been used already)
                    _calculationRepository.InsertCalculation(calculation);

                    // ------------
                    // Add request to queue and let the queue processor handle it.
                    // ...
                    var calculationRequestedEvent = new CalculationRequestedEvent
                    {
                        TransactionId = transactionId
                    };
                    _bus.Publish(calculationRequestedEvent);
                    Log.Logger.Information("Message broadcasted that calculation is requested: {@message}", calculationRequestedEvent);

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