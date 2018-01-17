namespace EventPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingInGroupToEventsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupToEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupToEvents", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupToEvents", "EventId", "dbo.Events");
            DropIndex("dbo.GroupToEvents", new[] { "EventId" });
            DropIndex("dbo.GroupToEvents", new[] { "GroupId" });
            DropTable("dbo.GroupToEvents");
        }
    }
}
