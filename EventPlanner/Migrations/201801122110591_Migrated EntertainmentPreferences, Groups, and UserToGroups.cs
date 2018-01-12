namespace EventPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigratedEntertainmentPreferencesGroupsandUserToGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EntertainmentPreferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PreferenePoints = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        GroupId = c.Int(nullable: false),
                        EntertainmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entertainments", t => t.EntertainmentId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.GroupId)
                .Index(t => t.EntertainmentId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserToGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserToGroups", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserToGroups", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.EntertainmentPreferences", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EntertainmentPreferences", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.EntertainmentPreferences", "EntertainmentId", "dbo.Entertainments");
            DropIndex("dbo.UserToGroups", new[] { "GroupId" });
            DropIndex("dbo.UserToGroups", new[] { "UserId" });
            DropIndex("dbo.EntertainmentPreferences", new[] { "EntertainmentId" });
            DropIndex("dbo.EntertainmentPreferences", new[] { "GroupId" });
            DropIndex("dbo.EntertainmentPreferences", new[] { "UserId" });
            DropTable("dbo.UserToGroups");
            DropTable("dbo.Groups");
            DropTable("dbo.EntertainmentPreferences");
        }
    }
}
