using Ajf.CoreSolver.DbModels;

namespace Ajf.CoreSolver.Shared
{
    public class DbContextProviderForTest : IDbContextProvider
    {
        private readonly string _connectionString;

        public DbContextProviderForTest(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CoreSolverContext GetDbContext()
        {
            return new CoreSolverContext
            {
                Database =
                {
                    Connection =
                    {
                        ConnectionString = _connectionString
                    }
                }
            };
        }
    }
}