namespace DevFestEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeTitleLengthRestriction : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sessions", "Title", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sessions", "Title", c => c.String(maxLength: 50));
        }
    }
}
