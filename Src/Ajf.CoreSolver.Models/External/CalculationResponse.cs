using System;

namespace Ajf.CoreSolver.Models.External
{
    public class CalculationResponse
    {
        public Guid TransactionId { get; set; }
        public CalculationStatus CalculationStatus { get; set; }
    }
}