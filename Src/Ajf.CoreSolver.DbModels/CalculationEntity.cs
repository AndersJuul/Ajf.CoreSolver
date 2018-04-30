using System;
using System.ComponentModel.DataAnnotations;

namespace Ajf.CoreSolver.DbModels
{
    public class CalculationEntity
    {
        [Key]
        public Guid TransactionId { get; set; }
    }
}