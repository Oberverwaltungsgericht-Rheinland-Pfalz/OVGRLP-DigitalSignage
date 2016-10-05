namespace DigitalSignage.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class FirstRelease : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.Basics",
          c => new
          {
            Nummer = c.Int(nullable: false, identity: true),
            Gerichtsname = c.String(),
            Kuerzel = c.String(),
            toXMLFullPath = c.String(),
            xsltFullPath = c.String(),
            globalXMLFullPath = c.String(),
          })
          .PrimaryKey(t => t.Nummer);

      CreateTable(
          "dbo.Besetzung",
          c => new
          {
            BesetzungsId = c.Int(nullable: false, identity: true),
            VerfahrensId = c.Int(nullable: false),
            Richter = c.String(nullable: false, maxLength: 255),
          })
          .PrimaryKey(t => t.BesetzungsId)
          .ForeignKey("dbo.Verfahren", t => t.VerfahrensId, cascadeDelete: true)
          .Index(t => t.VerfahrensId);

      CreateTable(
          "dbo.Displays",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            Name = c.String(),
            Title = c.String(),
            Template = c.String(),
            Styles = c.String(),
            Filter = c.String(),
            Group = c.String(),
            ControlUrl = c.String(),
            NetAddress = c.String(),
            WolIpAddress = c.String(),
            WolMacAddress = c.String(),
            WolUdpPort = c.Int(nullable: false),
            Description = c.String(),
          })
          .PrimaryKey(t => t.Id);

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

      CreateTable(
          "dbo.Stammdaten",
          c => new
          {
            StammdatenId = c.Int(nullable: false, identity: true),
            Gerichtsname = c.String(nullable: false, maxLength: 255),
            Datum = c.String(nullable: false, maxLength: 255),
          })
          .PrimaryKey(t => t.StammdatenId);

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
          .PrimaryKey(t => t.VerfahrensId)
          .ForeignKey("dbo.Stammdaten", t => t.StammdatenId, cascadeDelete: true)
          .Index(t => t.StammdatenId);
    }

    public override void Down()
    {
      DropForeignKey("dbo.Verfahren", "StammdatenId", "dbo.Stammdaten");
      DropForeignKey("dbo.ProzBevPassiv", "VerfahrensId", "dbo.Verfahren");
      DropForeignKey("dbo.ProzBevBeigeladen", "VerfahrensId", "dbo.Verfahren");
      DropForeignKey("dbo.ProzBevAktiv", "VerfahrensId", "dbo.Verfahren");
      DropForeignKey("dbo.ParteienZeugen", "VerfahrensId", "dbo.Verfahren");
      DropForeignKey("dbo.ParteienSV", "VerfahrensId", "dbo.Verfahren");
      DropForeignKey("dbo.ParteienPassiv", "VerfahrensId", "dbo.Verfahren");
      DropForeignKey("dbo.ParteienBeigeladen", "VerfahrensId", "dbo.Verfahren");
      DropForeignKey("dbo.ParteienAktiv", "VerfahrensId", "dbo.Verfahren");
      DropForeignKey("dbo.Besetzung", "VerfahrensId", "dbo.Verfahren");
      DropIndex("dbo.Verfahren", new[] { "StammdatenId" });
      DropIndex("dbo.ProzBevPassiv", new[] { "VerfahrensId" });
      DropIndex("dbo.ProzBevBeigeladen", new[] { "VerfahrensId" });
      DropIndex("dbo.ProzBevAktiv", new[] { "VerfahrensId" });
      DropIndex("dbo.ParteienZeugen", new[] { "VerfahrensId" });
      DropIndex("dbo.ParteienSV", new[] { "VerfahrensId" });
      DropIndex("dbo.ParteienPassiv", new[] { "VerfahrensId" });
      DropIndex("dbo.ParteienBeigeladen", new[] { "VerfahrensId" });
      DropIndex("dbo.ParteienAktiv", new[] { "VerfahrensId" });
      DropIndex("dbo.Besetzung", new[] { "VerfahrensId" });
      DropTable("dbo.Verfahren");
      DropTable("dbo.Stammdaten");
      DropTable("dbo.ProzBevPassiv");
      DropTable("dbo.ProzBevBeigeladen");
      DropTable("dbo.ProzBevAktiv");
      DropTable("dbo.ParteienZeugen");
      DropTable("dbo.ParteienSV");
      DropTable("dbo.ParteienPassiv");
      DropTable("dbo.ParteienBeigeladen");
      DropTable("dbo.ParteienAktiv");
      DropTable("dbo.Displays");
      DropTable("dbo.Besetzung");
      DropTable("dbo.Basics");
    }
  }
}