using Ajf.CoreSolver.DbModels;

namespace Ajf.CoreSolver.Shared
{
    public class DbContextProviderForTest : IDbContextProvider
    {
        private readonly CoreSolverContext _dbContext;

        public DbContextProviderForTest(CoreSolverContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CoreSolverContext GetDbContext()
        {
            return _dbContext;
        }
    }
}