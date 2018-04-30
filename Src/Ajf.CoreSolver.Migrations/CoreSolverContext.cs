using System.Data.Entity;

namespace Ajf.CoreSolver.Migrations
{
    public class CoreSolverContext : DbContext
    {
        public CoreSolverContext():base("CoreSolverConnection")
        {
            
        }
        public DbSet<CalculationEntity> Blogs { get; set; }
    }
}