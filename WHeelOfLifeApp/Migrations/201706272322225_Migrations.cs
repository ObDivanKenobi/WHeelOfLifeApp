namespace WHeelOfLifeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "IsDone", c => c.Boolean());   
        }
        
        public override void Down()
        {
        }
    }
}
