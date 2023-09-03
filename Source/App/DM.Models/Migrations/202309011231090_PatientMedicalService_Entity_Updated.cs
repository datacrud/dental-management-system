namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientMedicalService_Entity_Updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientMedicalService", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientMedicalService", "Quantity");
        }
    }
}
