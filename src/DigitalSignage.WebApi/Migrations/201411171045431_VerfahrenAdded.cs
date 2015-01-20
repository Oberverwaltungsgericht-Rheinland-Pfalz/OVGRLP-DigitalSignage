namespace DigitalSignage.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VerfahrenAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Verfahren",
                c => new
                    {
                        VerfahrensId = c.Int(nullable: false, identity: true),
                        StammdatenId = c.Int(nullable: false),
                        Lfdnr = c.Byte(nullable: false),
                        Kammer = c.Byte(nullable: false),
                        Sitzungssaal = c.String(nullable: false, maxLength: 255),
                        UhrzeitPlan = c.String(nullable: false, maxLength: 255),
                        UhrzeitAktuell = c.String(nullable: false, maxLength: 255),
                        Status = c.String(maxLength: 255),
                        Oeffentlich = c.String(nullable: false, maxLength: 255),
                        Az = c.String(nullable: false, maxLength: 255),
                        Gegenstand = c.String(nullable: false, maxLength: 255),
                        Bemerkung1 = c.String(maxLength: 255),
                        Bemerkung2 = c.String(maxLength: 255),
                        Art = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.VerfahrensId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Verfahren");
        }
    }
}
