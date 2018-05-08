using System;
using System.ComponentModel.DataAnnotations;

namespace Ajf.CoreSolver.DbModels
{
    public class CalculationEntity
    {
        [Key]
        public Guid TransactionId { get; set; }

        public CalculationStatusDto CalculationStatus { get; set; }
        public DateTime LatestUpdate { get; set; }
    }
}