using System;
using System.ComponentModel.DataAnnotations;

namespace Ajf.CoreSolver.Migrations
{
    public class CalculationEntity
    {
        [Key]
        public Guid TransactionId { get; set; }
    }
}