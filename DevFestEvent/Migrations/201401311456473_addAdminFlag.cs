namespace DevFestEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAdminFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "Admin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppUsers", "Admin");
        }
    }
}
