namespace ProductAlert.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BatchAlerts", "Batch", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BatchAlerts", "Batch");
        }
    }
}
