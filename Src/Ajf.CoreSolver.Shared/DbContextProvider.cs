using Ajf.CoreSolver.DbModels;

namespace Ajf.CoreSolver.Shared
{
    public class DbContextProvider : IDbContextProvider
    {
        public CoreSolverContext GetDbContext()
        {
            return new CoreSolverContext();
        }
    }
}