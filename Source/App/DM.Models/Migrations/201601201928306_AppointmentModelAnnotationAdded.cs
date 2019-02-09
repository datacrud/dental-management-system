namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppointmentModelAnnotationAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointment", "PatientNameOrId", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointment", "PatientNameOrId", c => c.String());
        }
    }
}
