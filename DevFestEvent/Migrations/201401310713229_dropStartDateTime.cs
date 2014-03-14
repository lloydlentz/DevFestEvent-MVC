namespace DevFestEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropStartDateTime : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sessions", "Start");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sessions", "Start", c => c.DateTime(nullable: false));
        }
    }
}
