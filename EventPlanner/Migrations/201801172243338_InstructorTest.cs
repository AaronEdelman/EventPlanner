namespace EventPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstructorTest : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            DropColumn("dbo.Entertainments", "IsAgeEighteenPlus");
            DropColumn("dbo.Entertainments", "IsDisabledFriendly");
            DropColumn("dbo.Entertainments", "IsOutdoors");
        }
    }
}
