using System.Collections.Generic;
using System.Linq;
using Ajf.CoreSolver.Models.External;

namespace Ajf.CoreSolver.Shared.Validation
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
                    new ValidationItem {Level = ValidationLevel.Error, Comment = "Unit must be supplied."});

            if (string.IsNullOrEmpty(calculationRequest.AlgorithmSelector))

                switch (calculationRequest.AlgorithmSelector)
                {
                    // Do not accept null or empty
                    case null:
                    case "":
                        validationList.Add(
                            new ValidationItem
                            {
                                Level = ValidationLevel.Error,
                                Comment = "Desired algorithm for calculation must be supplied."
                            });
                        break;

                    // Accept these
                    //case "MACRO":
                    case "CLOCKWISE":
                    //case "COUNTERCLOCKWISE":
                        break; // Valid

                    // Any other is invalid
                    default:
                        validationList.Add(
                            new ValidationItem
                            {
                                Level = ValidationLevel.Error,
                                Comment = "Desired algorithm for calculation must be supplied."
                            });
                        break;
                }

            return new ValidationResult
            {
                IsValid = !validationList.Any(),
                ValidationItems = validationList.ToArray()
            };
        }
    }
}