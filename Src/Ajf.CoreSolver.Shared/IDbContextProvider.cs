using System;
using Ajf.CoreSolver.DbModels;

namespace Ajf.CoreSolver.Shared
{
    public interface IDbContextProvider
    {
        CoreSolverContext GetDbContext();
    }
}