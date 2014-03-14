namespace DevFestEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Start = c.DateTime(nullable: false),
                        LengthMin = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Speakers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SpeakerSessions",
                c => new
                    {
                        Speaker_ID = c.Int(nullable: false),
                        Session_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Speaker_ID, t.Session_ID })
                .ForeignKey("dbo.Speakers", t => t.Speaker_ID, cascadeDelete: true)
                .ForeignKey("dbo.Sessions", t => t.Session_ID, cascadeDelete: true)
                .Index(t => t.Speaker_ID)
                .Index(t => t.Session_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpeakerSessions", "Session_ID", "dbo.Sessions");
            DropForeignKey("dbo.SpeakerSessions", "Speaker_ID", "dbo.Speakers");
            DropIndex("dbo.SpeakerSessions", new[] { "Session_ID" });
            DropIndex("dbo.SpeakerSessions", new[] { "Speaker_ID" });
            DropTable("dbo.SpeakerSessions");
            DropTable("dbo.Speakers");
            DropTable("dbo.Sessions");
        }
    }
}
