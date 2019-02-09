namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppointmentModelModefiedAndDoctorModelAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointment", "PatientNameOrId", c => c.String());
            AddColumn("dbo.Appointment", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Appointment", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointment", "StatusId");
            AddForeignKey("dbo.Appointment", "StatusId", "dbo.Status", "Id", cascadeDelete: false);
            DropColumn("dbo.Appointment", "PatientName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointment", "PatientName", c => c.String());
            DropForeignKey("dbo.Appointment", "StatusId", "dbo.Status");
            DropIndex("dbo.Appointment", new[] { "StatusId" });
            DropColumn("dbo.Appointment", "StatusId");
            DropColumn("dbo.Appointment", "Date");
            DropColumn("dbo.Appointment", "PatientNameOrId");
        }
    }
}
