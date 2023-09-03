namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientMedicalInfo_Entity_Updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientMedicalInfo", "MedicalInfoId", c => c.Guid(nullable: false));
            DropColumn("dbo.PatientMedicalInfo", "MedicalInfo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PatientMedicalInfo", "MedicalInfo", c => c.Guid(nullable: false));
            DropColumn("dbo.PatientMedicalInfo", "MedicalInfoId");
        }
    }
}
