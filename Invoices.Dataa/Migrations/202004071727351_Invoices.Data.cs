namespace Invoices.Dataa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoicesData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChargeName",
                c => new
                    {
                        ChargeNameId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ChargeNameId);
            
            CreateTable(
                "dbo.Charge",
                c => new
                    {
                        ChargeId = c.Int(nullable: false, identity: true),
                        Rate = c.Double(nullable: false),
                        Units = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Tax = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        ChargeNameId = c.Int(nullable: false),
                        InvoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChargeId)
                .ForeignKey("dbo.ChargeName", t => t.ChargeNameId, cascadeDelete: true)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.ChargeNameId)
                .Index(t => t.InvoiceId);
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.String(maxLength: 450),
                        InvoiceDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CompanyName = c.String(maxLength: 450),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .Index(t => new { t.CompanyName, t.InvoiceNumber }, unique: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        City = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        ContactPerson = c.String(),
                    })
                .PrimaryKey(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoice", "ClientId", "dbo.Client");
            DropForeignKey("dbo.Charge", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.Charge", "ChargeNameId", "dbo.ChargeName");
            DropIndex("dbo.Invoice", new[] { "ClientId" });
            DropIndex("dbo.Invoice", new[] { "CompanyName", "InvoiceNumber" });
            DropIndex("dbo.Charge", new[] { "InvoiceId" });
            DropIndex("dbo.Charge", new[] { "ChargeNameId" });
            DropTable("dbo.Client");
            DropTable("dbo.Invoice");
            DropTable("dbo.Charge");
            DropTable("dbo.ChargeName");
        }
    }
}
