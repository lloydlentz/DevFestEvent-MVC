namespace DevFestEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCustomURL2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sessions", "CustomURL", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sessions", "CustomURL", c => c.String());
        }
    }
}
