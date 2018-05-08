using System.Collections.Generic;
using System.Linq;
using Ajf.CoreSolver.Models.External;

namespace Ajf.CoreSolver.Shared
{
    /// <summary>
    /// </summary>
    public class CalculationRequestValidator : ICalculationRequestValidator
    {
        public IValidationResult Validate(CalculationRequest calculationRequest)
        {
            var validationList = new List<IValidationItem>();

            if (calculationRequest.Unit == null)
                validationList.Add(
                    new ValidationItem {Level = ValidationLevel.Error, Comment = "Unit must be supplied"});

            return new ValidationResult
            {
                IsValid = !validationList.Any(),
                ValidationItems = validationList.ToArray()
            };
        }
    }
}