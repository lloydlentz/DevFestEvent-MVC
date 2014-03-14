namespace DevFestEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStartMinute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "StartMinute", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "StartMinute");
        }
    }
}
