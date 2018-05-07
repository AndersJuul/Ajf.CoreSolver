using System.Threading.Tasks;
using Ajf.CoreSolver.Shared.QueueEvents;

namespace Ajf.CoreSolver.Service
{
    public interface IHandleCalculationRequested
    {
        Task Handle(CalculationRequestedEvent message);
    }
}