namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrescriptionModelModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prescription", "TotalCharge", c => c.Double(nullable: false));
            AddColumn("dbo.Prescription", "DiscountPercent", c => c.Double(nullable: false));
            AddColumn("dbo.Prescription", "DiscountAmount", c => c.Double(nullable: false));
            AddColumn("dbo.Prescription", "TotalPayable", c => c.Double(nullable: false));
            AddColumn("dbo.Prescription", "TotalPaid", c => c.Double(nullable: false));
            AddColumn("dbo.Prescription", "TotalDue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prescription", "TotalDue");
            DropColumn("dbo.Prescription", "TotalPaid");
            DropColumn("dbo.Prescription", "TotalPayable");
            DropColumn("dbo.Prescription", "DiscountAmount");
            DropColumn("dbo.Prescription", "DiscountPercent");
            DropColumn("dbo.Prescription", "TotalCharge");
        }
    }
}
