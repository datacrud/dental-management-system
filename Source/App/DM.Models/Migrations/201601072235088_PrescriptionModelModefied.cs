namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrescriptionModelModefied : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Prescription", new[] { "Code" });
            AlterColumn("dbo.Prescription", "Code", c => c.String(nullable: false, maxLength: 18));
            CreateIndex("dbo.Prescription", "Code", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Prescription", new[] { "Code" });
            AlterColumn("dbo.Prescription", "Code", c => c.String(nullable: false, maxLength: 18));
            CreateIndex("dbo.Prescription", "Code", unique: true);
        }
    }
}
