using Ajf.CoreSolver.Models.External;

namespace Ajf.CoreSolver.Shared.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICalculationRequestValidator
    {
        IValidationResult Validate(CalculationRequest calculationRequest);
    }
}