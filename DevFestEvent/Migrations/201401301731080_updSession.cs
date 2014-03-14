namespace DevFestEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updSession : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "Description", c => c.String());
            AddColumn("dbo.Sessions", "Room", c => c.String());
            AddColumn("dbo.Sessions", "SpeakerDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "SpeakerDescription");
            DropColumn("dbo.Sessions", "Room");
            DropColumn("dbo.Sessions", "Description");
        }
    }
}
