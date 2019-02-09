namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientModelModified : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Patient", new[] { "Code" });
            AlterColumn("dbo.Patient", "Code", c => c.String(maxLength: 8));
            CreateIndex("dbo.Patient", "Code", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Patient", new[] { "Code" });
            AlterColumn("dbo.Patient", "Code", c => c.String(nullable: false, maxLength: 8));
            CreateIndex("dbo.Patient", "Code", unique: true);
        }
    }
}
