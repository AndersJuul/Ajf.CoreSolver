using System.Diagnostics.CodeAnalysis;
using Ajf.CoreSolver.DbModels;

namespace Ajf.CoreSolver.Shared
{
    /// <summary>
    /// Returns a DbContext based on config settings
    /// -- the 'normal use' in the application. This
    /// functionality, though simple, has been stubbed out
    /// to allow for an alternative implementation of interface
    /// to be used in automated tests. As encapsulaton of
    /// hard-to-test code, it makes no sense to include it
    /// in coverage. Correct behaviour is instead ensured
    /// by code review.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DbContextProvider : IDbContextProvider
    {
        public CoreSolverContext GetDbContext()
        {
            return new CoreSolverContext();
        }
    }
}