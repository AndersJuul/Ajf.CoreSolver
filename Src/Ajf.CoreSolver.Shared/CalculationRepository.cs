using System;
using System.Linq;
using Ajf.CoreSolver.DbModels;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Models.External;
using Ajf.CoreSolver.Models.Internal;
using AutoMapper;
using Serilog;

namespace Ajf.CoreSolver.Shared
{
    /// <summary>
    /// </summary>
    public class CalculationRepository : ICalculationRepository
    {
        private readonly IDbContextProvider _dbContextProvider;
        private readonly IMapper _mapper;

        public CalculationRepository(IDbContextProvider dbContextProvider, IMapper mapper)
        {
            _dbContextProvider = dbContextProvider;
            _mapper = mapper;
        }

        public void InsertCalculation(Calculation calculation)
        {
            var calculationDto = _mapper.Map<Calculation, CalculationEntity>(calculation);
            calculationDto.LatestUpdate = DateTime.Now;

            using (var context = _dbContextProvider.GetDbContext())
            {
                context.Calculations.Add(calculationDto);

                context.SaveChanges();
            }
        }

        public CalculationStatus GetCalculationStatus(Guid transactionId)
        {
            using (var context = _dbContextProvider.GetDbContext())
            {
                var calculationDto =
                    context
                        .Calculations
                        .Single(x => x.TransactionId == transactionId);

                var calculationStatus = _mapper
                    .Map<CalculationStatusDto, CalculationStatus>(calculationDto.CalculationStatus);

                return calculationStatus;
            }
        }

        public void SetCalculationStatus(Guid transactionId, CalculationStatus calculationStatus)
        {
            var calculationStatusDto = _mapper
                .Map<CalculationStatus, CalculationStatusDto>(calculationStatus);
            using (var context = _dbContextProvider.GetDbContext())
            {
                Log.Logger.Debug("SetCalculationStatus: " + context.Database.Connection.ConnectionString);

                var calculationDto =
                    context
                        .Calculations
                        .Single(x => x.TransactionId == transactionId);
                calculationDto.CalculationStatus = calculationStatusDto;
                calculationDto.LatestUpdate = DateTime.Now;
                context.SaveChanges();
            }
        }
    }
}