using Ajf.CoreSolver.DbModels;
using Ajf.CoreSolver.Models;

namespace Ajf.CoreSolver.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public class CalculationRepository : ICalculationRepository
    {
        private readonly IDbContextProvider _dbContextProvider;

        public CalculationRepository(IDbContextProvider dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }
        public void InsertCalculation(CalculationRequest calculationRequest)
        {
            using (var context = _dbContextProvider.GetDbContext())
            {

            }
        }
    }
}