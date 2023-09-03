namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedicalInfo_Entity_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicalInfo",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Created = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MedicalInfo", new[] { "Name" });
            DropIndex("dbo.MedicalInfo", new[] { "Id" });
            DropTable("dbo.MedicalInfo");
        }
    }
}
