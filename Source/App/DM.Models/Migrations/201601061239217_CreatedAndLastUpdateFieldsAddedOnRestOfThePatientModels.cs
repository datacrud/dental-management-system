namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedAndLastUpdateFieldsAddedOnRestOfThePatientModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patient", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Patient", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Prescription", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Prescription", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PatientMedicalService", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.PatientMedicalService", "LastUpdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientMedicalService", "LastUpdate");
            DropColumn("dbo.PatientMedicalService", "Created");
            DropColumn("dbo.Prescription", "LastUpdate");
            DropColumn("dbo.Prescription", "Created");
            DropColumn("dbo.Patient", "LastUpdate");
            DropColumn("dbo.Patient", "Created");
        }
    }
}
