using Ajf.CoreSolver.Models;

namespace Ajf.CoreSolver.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public class CalculationRequestValidator : ICalculationRequestValidator
    {
        public IValidationResult Validate(CalculationRequest calculationRequest)
        {
            return new ValidationResult
            {
                IsValid = true
            };
        }
    }
}