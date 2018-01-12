namespace EventPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOtherModels : DbMigration
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
                "dbo.Entertainments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Restriction = c.String(),
                        VenueId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId)
                .ForeignKey("dbo.Venues", t => t.VenueId)
                .Index(t => t.VenueId)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Cost = c.Int(nullable: false),
                        Weblink = c.String(),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        IsDisabledFriendly = c.Boolean(nullable: false),
                        IsOutdoors = c.Boolean(nullable: false),
                        HasSeating = c.Boolean(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId)
                .Index(t => t.EventId);
            
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
            DropForeignKey("dbo.Entertainments", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Venues", "EventId", "dbo.Events");
            DropForeignKey("dbo.Entertainments", "EventId", "dbo.Events");
            DropForeignKey("dbo.Events", "AddressId", "dbo.Addresses");
            DropIndex("dbo.UserToGroups", new[] { "GroupId" });
            DropIndex("dbo.UserToGroups", new[] { "UserId" });
            DropIndex("dbo.Venues", new[] { "EventId" });
            DropIndex("dbo.Events", new[] { "AddressId" });
            DropIndex("dbo.Entertainments", new[] { "EventId" });
            DropIndex("dbo.Entertainments", new[] { "VenueId" });
            DropIndex("dbo.EntertainmentPreferences", new[] { "EntertainmentId" });
            DropIndex("dbo.EntertainmentPreferences", new[] { "GroupId" });
            DropIndex("dbo.EntertainmentPreferences", new[] { "UserId" });
            DropTable("dbo.UserToGroups");
            DropTable("dbo.Groups");
            DropTable("dbo.Venues");
            DropTable("dbo.Events");
            DropTable("dbo.Entertainments");
            DropTable("dbo.EntertainmentPreferences");
        }
    }
}
