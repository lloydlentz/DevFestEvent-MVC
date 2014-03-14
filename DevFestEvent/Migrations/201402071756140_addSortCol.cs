namespace DevFestEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSortCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "Sort", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "Sort");
        }
    }
}
