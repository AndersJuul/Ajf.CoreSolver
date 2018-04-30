using System.Data.Entity.Migrations;
using Ajf.CoreSolver.DbModels;

namespace Ajf.CoreSolver.Migrations.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CoreSolverContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CoreSolverContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}