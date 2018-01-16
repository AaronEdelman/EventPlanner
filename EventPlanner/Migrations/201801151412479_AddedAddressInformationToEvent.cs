namespace EventPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAddressInformationToEvent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Events", new[] { "AddressId" });
            AddColumn("dbo.Events", "AddressNumber", c => c.String());
            AddColumn("dbo.Events", "Street", c => c.String());
            AddColumn("dbo.Events", "City", c => c.String());
            AddColumn("dbo.Events", "State", c => c.String());
            AddColumn("dbo.Events", "ZipCode", c => c.String());
            AddColumn("dbo.Events", "PromoterId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Events", "PromoterId");
            AddForeignKey("dbo.Events", "PromoterId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Events", "AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "AddressId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Events", "PromoterId", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "PromoterId" });
            DropColumn("dbo.Events", "PromoterId");
            DropColumn("dbo.Events", "ZipCode");
            DropColumn("dbo.Events", "State");
            DropColumn("dbo.Events", "City");
            DropColumn("dbo.Events", "Street");
            DropColumn("dbo.Events", "AddressNumber");
            CreateIndex("dbo.Events", "AddressId");
            AddForeignKey("dbo.Events", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
    }
}
