namespace DevFestEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCustomURL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "customURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "customURL");
        }
    }
}
