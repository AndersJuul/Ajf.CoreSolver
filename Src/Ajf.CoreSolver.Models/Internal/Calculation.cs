﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ajf.CoreSolver.Models.External;

namespace Ajf.CoreSolver.Models.Internal
{
    public class Calculation 
    {
        public Guid TransactionId { get; set; }
        public CalculationStatus CalculationStatus { get; set; }
    }
}
