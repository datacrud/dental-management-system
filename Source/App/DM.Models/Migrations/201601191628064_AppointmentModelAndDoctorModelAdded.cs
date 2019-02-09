namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppointmentModelAndDoctorModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointment",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Code = c.String(),
                        PatientName = c.String(),
                        Age = c.Int(nullable: false),
                        Phone = c.String(),
                        Time = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        DoctorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctor", t => t.DoctorId, cascadeDelete: true)
                .Index(t => t.Id, unique: true)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.Doctor",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Phone = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointment", "DoctorId", "dbo.Doctor");
            DropIndex("dbo.Doctor", new[] { "Id" });
            DropIndex("dbo.Appointment", new[] { "DoctorId" });
            DropIndex("dbo.Appointment", new[] { "Id" });
            DropTable("dbo.Doctor");
            DropTable("dbo.Appointment");
        }
    }
}
