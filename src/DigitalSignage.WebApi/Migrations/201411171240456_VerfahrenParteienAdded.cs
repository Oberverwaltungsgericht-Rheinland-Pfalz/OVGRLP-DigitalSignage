namespace DigitalSignage.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VerfahrenParteienAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParteienAktiv",
                c => new
                    {
                        ParteiId = c.Int(nullable: false, identity: true),
                        VerfahrensId = c.Int(nullable: false),
                        Partei = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ParteiId)
                .ForeignKey("dbo.Verfahren", t => t.VerfahrensId, cascadeDelete: true)
                .Index(t => t.VerfahrensId);
            
            CreateTable(
                "dbo.ParteienBeigeladen",
                c => new
                    {
                        ParteiId = c.Int(nullable: false, identity: true),
                        VerfahrensId = c.Int(nullable: false),
                        Partei = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ParteiId)
                .ForeignKey("dbo.Verfahren", t => t.VerfahrensId, cascadeDelete: true)
                .Index(t => t.VerfahrensId);
            
            CreateTable(
                "dbo.ParteienPassiv",
                c => new
                    {
                        ParteiId = c.Int(nullable: false, identity: true),
                        VerfahrensId = c.Int(nullable: false),
                        Partei = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ParteiId)
                .ForeignKey("dbo.Verfahren", t => t.VerfahrensId, cascadeDelete: true)
                .Index(t => t.VerfahrensId);
            
            CreateTable(
                "dbo.ParteienSV",
                c => new
                    {
                        ParteiId = c.Int(nullable: false, identity: true),
                        VerfahrensId = c.Int(nullable: false),
                        Partei = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ParteiId)
                .ForeignKey("dbo.Verfahren", t => t.VerfahrensId, cascadeDelete: true)
                .Index(t => t.VerfahrensId);
            
            CreateTable(
                "dbo.ParteienZeugen",
                c => new
                    {
                        ParteiId = c.Int(nullable: false, identity: true),
                        VerfahrensId = c.Int(nullable: false),
                        Partei = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ParteiId)
                .ForeignKey("dbo.Verfahren", t => t.VerfahrensId, cascadeDelete: true)
                .Index(t => t.VerfahrensId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParteienZeugen", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ParteienSV", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ParteienPassiv", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ParteienBeigeladen", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ParteienAktiv", "VerfahrensId", "dbo.Verfahren");
            DropIndex("dbo.ParteienZeugen", new[] { "VerfahrensId" });
            DropIndex("dbo.ParteienSV", new[] { "VerfahrensId" });
            DropIndex("dbo.ParteienPassiv", new[] { "VerfahrensId" });
            DropIndex("dbo.ParteienBeigeladen", new[] { "VerfahrensId" });
            DropIndex("dbo.ParteienAktiv", new[] { "VerfahrensId" });
            DropTable("dbo.ParteienZeugen");
            DropTable("dbo.ParteienSV");
            DropTable("dbo.ParteienPassiv");
            DropTable("dbo.ParteienBeigeladen");
            DropTable("dbo.ParteienAktiv");
        }
    }
}
