namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientEntity_Updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patient", "Gender", c => c.String());
            AddColumn("dbo.Patient", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patient", "Note");
            DropColumn("dbo.Patient", "Gender");
        }
    }
}
