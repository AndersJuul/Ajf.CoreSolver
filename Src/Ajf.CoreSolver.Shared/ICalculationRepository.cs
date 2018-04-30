using Ajf.CoreSolver.Models;

namespace Ajf.CoreSolver.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICalculationRepository
    {
        void InsertCalculation(CalculationRequest calculationRequest);
    }
}