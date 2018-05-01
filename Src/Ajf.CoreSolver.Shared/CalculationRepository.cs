﻿using Ajf.CoreSolver.DbModels;
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
    }
}