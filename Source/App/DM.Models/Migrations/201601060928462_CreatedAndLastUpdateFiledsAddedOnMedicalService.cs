namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedAndLastUpdateFiledsAddedOnMedicalService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalService", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.MedicalService", "LastUpdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MedicalService", "LastUpdate");
            DropColumn("dbo.MedicalService", "Created");
        }
    }
}
