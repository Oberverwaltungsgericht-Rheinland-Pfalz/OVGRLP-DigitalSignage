namespace DigitalSignage.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VerfahrenProzBevsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProzBevAktiv",
                c => new
                    {
                        ProzBevAktivID = c.Int(nullable: false, identity: true),
                        VerfahrensId = c.Int(nullable: false),
                        PB = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ProzBevAktivID)
                .ForeignKey("dbo.Verfahren", t => t.VerfahrensId, cascadeDelete: true)
                .Index(t => t.VerfahrensId);
            
            CreateTable(
                "dbo.ProzBevBeigeladen",
                c => new
                    {
                        ProzBevAktivID = c.Int(nullable: false, identity: true),
                        VerfahrensId = c.Int(nullable: false),
                        PB = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ProzBevAktivID)
                .ForeignKey("dbo.Verfahren", t => t.VerfahrensId, cascadeDelete: true)
                .Index(t => t.VerfahrensId);
            
            CreateTable(
                "dbo.ProzBevPassiv",
                c => new
                    {
                        ProzBevAktivID = c.Int(nullable: false, identity: true),
                        VerfahrensId = c.Int(nullable: false),
                        PB = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ProzBevAktivID)
                .ForeignKey("dbo.Verfahren", t => t.VerfahrensId, cascadeDelete: true)
                .Index(t => t.VerfahrensId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProzBevPassiv", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ProzBevBeigeladen", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ProzBevAktiv", "VerfahrensId", "dbo.Verfahren");
            DropIndex("dbo.ProzBevPassiv", new[] { "VerfahrensId" });
            DropIndex("dbo.ProzBevBeigeladen", new[] { "VerfahrensId" });
            DropIndex("dbo.ProzBevAktiv", new[] { "VerfahrensId" });
            DropTable("dbo.ProzBevPassiv");
            DropTable("dbo.ProzBevBeigeladen");
            DropTable("dbo.ProzBevAktiv");
        }
    }
}
