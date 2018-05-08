using EasyNetQ;

namespace Ajf.CoreSolver.Shared.Service
{
    public interface IBusAdapter
    {
        IBus Bus { get; }
    }
}