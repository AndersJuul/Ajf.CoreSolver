﻿namespace Ajf.CoreSolver.Models
{
    public enum CalculationStatus
    {
        None=0,
        Queued=1,
        InProgress=2,
        DoneAndFailed=3,
        DoneAndSuccess=4
    }
}