namespace DevFestEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeDBGenID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "IID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "IID");
        }
    }
}
