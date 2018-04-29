using Ajf.CoreSolver.Models;

namespace Ajf.CoreSolver.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICalculationRequestValidator
    {
        IValidationResult Validate(CalculationRequest calculationRequest);
    }
}