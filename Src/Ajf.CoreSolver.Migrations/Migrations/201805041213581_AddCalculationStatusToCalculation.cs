namespace Ajf.CoreSolver.Migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCalculationStatusToCalculation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalculationEntities", "CalculationStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CalculationEntities", "CalculationStatus");
        }
    }
}
