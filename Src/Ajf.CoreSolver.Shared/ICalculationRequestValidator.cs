using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Models.External;

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