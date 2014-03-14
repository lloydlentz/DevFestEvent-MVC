namespace DevFestEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updSession1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "StartHour", c => c.Int(nullable: false));
            AddColumn("dbo.Sessions", "StartAMPM", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "StartAMPM");
            DropColumn("dbo.Sessions", "StartHour");
        }
    }
}
