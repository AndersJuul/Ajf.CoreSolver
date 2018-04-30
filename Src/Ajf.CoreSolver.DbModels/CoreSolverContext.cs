using System.Data.Entity;

namespace Ajf.CoreSolver.DbModels
{
    public class CoreSolverContext : DbContext
    {
        public CoreSolverContext():base("CoreSolverConnection")
        {
            
        }
        public DbSet<CalculationEntity> Calculations { get; set; }
    }
}