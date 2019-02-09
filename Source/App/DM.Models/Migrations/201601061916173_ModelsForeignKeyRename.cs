namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelsForeignKeyRename : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PatientMedicalService", name: "ServiceId", newName: "MedicalServiceId");
            RenameIndex(table: "dbo.PatientMedicalService", name: "IX_ServiceId", newName: "IX_MedicalServiceId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PatientMedicalService", name: "IX_MedicalServiceId", newName: "IX_ServiceId");
            RenameColumn(table: "dbo.PatientMedicalService", name: "MedicalServiceId", newName: "ServiceId");
        }
    }
}
