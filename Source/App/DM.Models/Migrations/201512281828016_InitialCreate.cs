namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventory",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        CashMemoNo = c.String(nullable: false),
                        OnHand = c.Int(nullable: false),
                        ReceivedOrShippedQuantity = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: false)
                .Index(t => t.Id, unique: true)
                .Index(t => t.ProductId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Code = c.String(maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 40),
                        StartingInventory = c.Int(nullable: false),
                        Received = c.Int(nullable: false),
                        Shipped = c.Int(nullable: false),
                        OnHand = c.Int(nullable: false),
                        MinimumRequired = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        SalePrice = c.Double(nullable: false),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: false)
                .Index(t => t.Id, unique: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 8),
                        Name = c.String(nullable: false, maxLength: 30),
                        Age = c.Int(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Prescription",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 18),
                        PatientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patient", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.Id, unique: true)
                .Index(t => t.Code, unique: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.PatientService",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PatientId = c.Guid(nullable: false),
                        PrescriptionId = c.Guid(nullable: false),
                        ServiceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patient", t => t.PatientId, cascadeDelete: false)
                .ForeignKey("dbo.Prescription", t => t.PrescriptionId, cascadeDelete: true)
                .ForeignKey("dbo.Service", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.Id, unique: true)
                .Index(t => t.PatientId)
                .Index(t => t.PrescriptionId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Service",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Code = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Charge = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true)
                .Index(t => t.Code, unique: true)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientService", "ServiceId", "dbo.Service");
            DropForeignKey("dbo.PatientService", "PrescriptionId", "dbo.Prescription");
            DropForeignKey("dbo.PatientService", "PatientId", "dbo.Patient");
            DropForeignKey("dbo.Prescription", "PatientId", "dbo.Patient");
            DropForeignKey("dbo.Inventory", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Product", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Inventory", "ProductId", "dbo.Product");
            DropIndex("dbo.Service", new[] { "Name" });
            DropIndex("dbo.Service", new[] { "Code" });
            DropIndex("dbo.Service", new[] { "Id" });
            DropIndex("dbo.PatientService", new[] { "ServiceId" });
            DropIndex("dbo.PatientService", new[] { "PrescriptionId" });
            DropIndex("dbo.PatientService", new[] { "PatientId" });
            DropIndex("dbo.PatientService", new[] { "Id" });
            DropIndex("dbo.Prescription", new[] { "PatientId" });
            DropIndex("dbo.Prescription", new[] { "Code" });
            DropIndex("dbo.Prescription", new[] { "Id" });
            DropIndex("dbo.Patient", new[] { "Code" });
            DropIndex("dbo.Patient", new[] { "Id" });
            DropIndex("dbo.Status", new[] { "Id" });
            DropIndex("dbo.Product", new[] { "StatusId" });
            DropIndex("dbo.Product", new[] { "Name" });
            DropIndex("dbo.Product", new[] { "Id" });
            DropIndex("dbo.Inventory", new[] { "StatusId" });
            DropIndex("dbo.Inventory", new[] { "ProductId" });
            DropIndex("dbo.Inventory", new[] { "Id" });
            DropTable("dbo.Service");
            DropTable("dbo.PatientService");
            DropTable("dbo.Prescription");
            DropTable("dbo.Patient");
            DropTable("dbo.Status");
            DropTable("dbo.Product");
            DropTable("dbo.Inventory");
        }
    }
}
