namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusIdFieldAddedOnPrescriptionModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prescription", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Prescription", "StatusId");
            AddForeignKey("dbo.Prescription", "StatusId", "dbo.Status", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prescription", "StatusId", "dbo.Status");
            DropIndex("dbo.Prescription", new[] { "StatusId" });
            DropColumn("dbo.Prescription", "StatusId");
        }
    }
}
