namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PrescriptionId = c.Guid(nullable: false),
                        Amount = c.Double(nullable: false),
                        Comment = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prescription", t => t.PrescriptionId, cascadeDelete: true)
                .Index(t => t.Id, unique: true)
                .Index(t => t.PrescriptionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payment", "PrescriptionId", "dbo.Prescription");
            DropIndex("dbo.Payment", new[] { "PrescriptionId" });
            DropIndex("dbo.Payment", new[] { "Id" });
            DropTable("dbo.Payment");
        }
    }
}
