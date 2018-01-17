namespace EventPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsOutdoorsIsDisabledFriendlyandIsAgeEighteenPlusboolstotheentertainmenttable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Entertainments", "IsOutdoors", c => c.Boolean(nullable: false));
            AddColumn("dbo.Entertainments", "IsDisabledFriendly", c => c.Boolean(nullable: false));
            AddColumn("dbo.Entertainments", "IsAgeEighteenPlus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Entertainments", "IsAgeEighteenPlus");
            DropColumn("dbo.Entertainments", "IsDisabledFriendly");
            DropColumn("dbo.Entertainments", "IsOutdoors");
        }
    }
}
