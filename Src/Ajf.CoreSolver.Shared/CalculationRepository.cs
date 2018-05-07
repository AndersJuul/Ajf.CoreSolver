using System;
using System.Linq;
using Ajf.CoreSolver.DbModels;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Models.Internal;
using AutoMapper;

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
                    .Map<CalculationStatusDto,CalculationStatus>(calculationDto.CalculationStatus);

                return calculationStatus;
            }
        }
    }
}