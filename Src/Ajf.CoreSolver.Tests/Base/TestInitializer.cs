using System.Data.Entity;
using Ajf.CoreSolver.DbModels;

namespace Ajf.CoreSolver.Tests.Base
{
    public class TestInitializer : DropCreateDatabaseAlways<CoreSolverContext>
    {
        public override void InitializeDatabase(CoreSolverContext context)
        {
            base.InitializeDatabase(context);

            context.SaveChanges();
        }
    }
}