namespace DevFestEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updSession2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "CSSClass", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "CSSClass");
        }
    }
}
