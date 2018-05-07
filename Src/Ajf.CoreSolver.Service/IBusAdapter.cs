using EasyNetQ;

namespace Ajf.CoreSolver.Service
{
    public interface IBusAdapter
    {
        IBus Bus { get; }
    }
}