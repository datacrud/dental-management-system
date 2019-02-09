namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedLastUpdateFieldAddedOnProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Product", "LastUpdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "LastUpdate");
            DropColumn("dbo.Product", "Created");
        }
    }
}
