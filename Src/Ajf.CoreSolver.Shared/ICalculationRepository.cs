using System;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Models.Internal;

namespace Ajf.CoreSolver.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICalculationRepository
    {
        void InsertCalculation(Calculation calculation);
        CalculationStatus GetCalculationStatus(Guid transactionId);
    }
}