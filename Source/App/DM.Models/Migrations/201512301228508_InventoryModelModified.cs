namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventoryModelModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventory", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Inventory", "LastUpdate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Inventory", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inventory", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Inventory", "LastUpdate");
            DropColumn("dbo.Inventory", "Created");
        }
    }
}
