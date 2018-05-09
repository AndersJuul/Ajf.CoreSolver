using System;
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

            ValidateUnit(calculationRequest, validationList);
            ValidateAlgorithmSelector(calculationRequest, validationList);

            return new ValidationResult
            {
                IsValid = !validationList.Any(),
                ValidationItems = validationList.ToArray()
            };
        }

        private static void ValidateAlgorithmSelector(CalculationRequest calculationRequest,
            List<IValidationItem> validationList)
        {
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
                //case "CLOCKWISE":
                case "COUNTERCLOCKWISE":
                    break; // Valid

                // Any other is invalid
                default:
                    validationList.Add(
                        new ValidationItem
                        {
                            Level = ValidationLevel.Error,
                            Comment = "Desired algorithm for calculation does not match existing."
                        });
                    break;
            }
        }

        private static void ValidateUnit(CalculationRequest calculationRequest, List<IValidationItem> validationList)
        {
            if (calculationRequest.Unit == null)
            {
                validationList.Add(
                    new ValidationItem {Level = ValidationLevel.Error, Comment = "Root Unit must be supplied."});
                return;
            }

            if (calculationRequest.Unit.SubUnits.Length != 4)
                validationList.Add(
                    new ValidationItem
                    {
                        Level = ValidationLevel.Error,
                        Comment = "Root Unit is expected to have four sub-units."
                    });

            ValidateUnit(calculationRequest.Unit, validationList, "/");

        }

        private static void ValidateUnit(Unit unit, List<IValidationItem> validationList, string context)
        {
            if (unit == null)
            {
                validationList.Add(
                    new ValidationItem { Level = ValidationLevel.Error, Comment = "Null Unit found at " + context });
                return;
            }

            if (unit.Id.Equals(Guid.Empty))
            {
                validationList.Add(
                    new ValidationItem { Level = ValidationLevel.Error, Comment =
                        $"Unit with empty ID found at {context}"
                    });
                return;
            }

            foreach (var subUnit in unit.SubUnits)
                ValidateUnit(subUnit, validationList, context + "/");
        }
    }
}