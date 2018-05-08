namespace Ajf.CoreSolver.Migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLatestTimeStamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalculationEntities", "LatestUpdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CalculationEntities", "LatestUpdate");
        }
    }
}
