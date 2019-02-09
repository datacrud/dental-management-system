namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientServiceRenameToPatientMedicalService : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PatientService", newName: "PatientMedicalService");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PatientMedicalService", newName: "PatientService");
        }
    }
}
