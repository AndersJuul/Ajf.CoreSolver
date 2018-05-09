using System;
using EasyNetQ;

namespace Ajf.CoreSolver.Shared.Service
{
    public interface IBusAdapter:IDisposable
    {
        IBus Bus { get; }
    }
}