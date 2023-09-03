namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prescription_Entity_Updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prescription", "FixedDiscount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prescription", "FixedDiscount");
        }
    }
}
