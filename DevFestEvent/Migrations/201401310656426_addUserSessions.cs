namespace DevFestEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserSessions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUserSessions",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.ID })
                .ForeignKey("dbo.AppUsers", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Sessions", t => t.ID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppUserSessions", "ID", "dbo.Sessions");
            DropForeignKey("dbo.AppUserSessions", "UserID", "dbo.AppUsers");
            DropIndex("dbo.AppUserSessions", new[] { "ID" });
            DropIndex("dbo.AppUserSessions", new[] { "UserID" });
            DropTable("dbo.AppUserSessions");
        }
    }
}
