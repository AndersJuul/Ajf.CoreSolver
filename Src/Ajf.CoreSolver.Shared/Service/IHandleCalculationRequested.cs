using System.Threading.Tasks;
using Ajf.CoreSolver.Shared.QueueEvents;

namespace Ajf.CoreSolver.Shared.Service
{
    public interface IHandleCalculationRequested
    {
        Task Handle(CalculationRequestedEvent message);
    }
}