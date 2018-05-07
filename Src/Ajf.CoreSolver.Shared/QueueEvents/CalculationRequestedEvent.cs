using System;

namespace Ajf.CoreSolver.Shared.QueueEvents
{
    public class CalculationRequestedEvent
    {
        public Guid TransactionId { get; set; }
    }
}