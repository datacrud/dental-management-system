namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientMedicalInfo_Entity_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientMedicalInfo",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PatientId = c.Guid(nullable: false),
                        MedicalInfo = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PatientMedicalInfo", new[] { "Id" });
            DropTable("dbo.PatientMedicalInfo");
        }
    }
}
