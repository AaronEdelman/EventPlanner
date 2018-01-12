namespace EventPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrate : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Venues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        IsDisabledFriendly = c.Boolean(nullable: false),
                        IsOutdoors = c.Boolean(nullable: false),
                        HasSeating = c.Boolean(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Events", t => t.EventId)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Entertainments", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Venues", "EventId", "dbo.Events");
            DropForeignKey("dbo.Entertainments", "EventId", "dbo.Events");
            DropIndex("dbo.Venues", new[] { "EventId" });
            DropIndex("dbo.Entertainments", new[] { "EventId" });
            DropIndex("dbo.Entertainments", new[] { "VenueId" });
            DropTable("dbo.Venues");
            DropTable("dbo.Entertainments");
        }
    }
}
