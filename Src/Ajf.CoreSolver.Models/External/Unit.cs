using System;

namespace Ajf.CoreSolver.Models.External
{
    public class Unit
    {
        public Guid Id;
        public Unit[] SubUnits;
        public Connection[] Connections;
    }
}