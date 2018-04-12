namespace DigitalSignage.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VerfahrenInt64 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Besetzung", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ParteienAktiv", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ParteienBeigeladen", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ParteienPassiv", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ParteienSV", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ParteienZeugen", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ProzBevAktiv", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ProzBevBeigeladen", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ProzBevPassiv", "VerfahrensId", "dbo.Verfahren");
            DropIndex("dbo.Besetzung", new[] { "VerfahrensId" });
            DropIndex("dbo.ParteienAktiv", new[] { "VerfahrensId" });
            DropIndex("dbo.ParteienBeigeladen", new[] { "VerfahrensId" });
            DropIndex("dbo.ParteienPassiv", new[] { "VerfahrensId" });
            DropIndex("dbo.ParteienSV", new[] { "VerfahrensId" });
            DropIndex("dbo.ParteienZeugen", new[] { "VerfahrensId" });
            DropIndex("dbo.ProzBevAktiv", new[] { "VerfahrensId" });
            DropIndex("dbo.ProzBevBeigeladen", new[] { "VerfahrensId" });
            DropIndex("dbo.ProzBevPassiv", new[] { "VerfahrensId" });
            DropPrimaryKey("dbo.Verfahren");
            AlterColumn("dbo.Besetzung", "VerfahrensId", c => c.Long(nullable: false));
            AlterColumn("dbo.ParteienAktiv", "VerfahrensId", c => c.Long(nullable: false));
            AlterColumn("dbo.ParteienBeigeladen", "VerfahrensId", c => c.Long(nullable: false));
            AlterColumn("dbo.ParteienPassiv", "VerfahrensId", c => c.Long(nullable: false));
            AlterColumn("dbo.ParteienSV", "VerfahrensId", c => c.Long(nullable: false));
            AlterColumn("dbo.ParteienZeugen", "VerfahrensId", c => c.Long(nullable: false));
            AlterColumn("dbo.ProzBevAktiv", "VerfahrensId", c => c.Long(nullable: false));
            AlterColumn("dbo.ProzBevBeigeladen", "VerfahrensId", c => c.Long(nullable: false));
            AlterColumn("dbo.ProzBevPassiv", "VerfahrensId", c => c.Long(nullable: false));
            AlterColumn("dbo.Verfahren", "VerfahrensId", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Verfahren", "SitzungssaalNr", c => c.Long());
            AddPrimaryKey("dbo.Verfahren", "VerfahrensId");
            CreateIndex("dbo.Besetzung", "VerfahrensId");
            CreateIndex("dbo.ParteienAktiv", "VerfahrensId");
            CreateIndex("dbo.ParteienBeigeladen", "VerfahrensId");
            CreateIndex("dbo.ParteienPassiv", "VerfahrensId");
            CreateIndex("dbo.ParteienSV", "VerfahrensId");
            CreateIndex("dbo.ParteienZeugen", "VerfahrensId");
            CreateIndex("dbo.ProzBevAktiv", "VerfahrensId");
            CreateIndex("dbo.ProzBevBeigeladen", "VerfahrensId");
            CreateIndex("dbo.ProzBevPassiv", "VerfahrensId");
            AddForeignKey("dbo.Besetzung", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ParteienAktiv", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ParteienBeigeladen", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ParteienPassiv", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ParteienSV", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ParteienZeugen", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ProzBevAktiv", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ProzBevBeigeladen", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ProzBevPassiv", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProzBevPassiv", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ProzBevBeigeladen", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ProzBevAktiv", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ParteienZeugen", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ParteienSV", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ParteienPassiv", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ParteienBeigeladen", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.ParteienAktiv", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.Besetzung", "VerfahrensId", "dbo.Verfahren");
            DropIndex("dbo.ProzBevPassiv", new[] { "VerfahrensId" });
            DropIndex("dbo.ProzBevBeigeladen", new[] { "VerfahrensId" });
            DropIndex("dbo.ProzBevAktiv", new[] { "VerfahrensId" });
            DropIndex("dbo.ParteienZeugen", new[] { "VerfahrensId" });
            DropIndex("dbo.ParteienSV", new[] { "VerfahrensId" });
            DropIndex("dbo.ParteienPassiv", new[] { "VerfahrensId" });
            DropIndex("dbo.ParteienBeigeladen", new[] { "VerfahrensId" });
            DropIndex("dbo.ParteienAktiv", new[] { "VerfahrensId" });
            DropIndex("dbo.Besetzung", new[] { "VerfahrensId" });
            DropPrimaryKey("dbo.Verfahren");
            AlterColumn("dbo.Verfahren", "SitzungssaalNr", c => c.Int());
            AlterColumn("dbo.Verfahren", "VerfahrensId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ProzBevPassiv", "VerfahrensId", c => c.Int(nullable: false));
            AlterColumn("dbo.ProzBevBeigeladen", "VerfahrensId", c => c.Int(nullable: false));
            AlterColumn("dbo.ProzBevAktiv", "VerfahrensId", c => c.Int(nullable: false));
            AlterColumn("dbo.ParteienZeugen", "VerfahrensId", c => c.Int(nullable: false));
            AlterColumn("dbo.ParteienSV", "VerfahrensId", c => c.Int(nullable: false));
            AlterColumn("dbo.ParteienPassiv", "VerfahrensId", c => c.Int(nullable: false));
            AlterColumn("dbo.ParteienBeigeladen", "VerfahrensId", c => c.Int(nullable: false));
            AlterColumn("dbo.ParteienAktiv", "VerfahrensId", c => c.Int(nullable: false));
            AlterColumn("dbo.Besetzung", "VerfahrensId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Verfahren", "VerfahrensId");
            CreateIndex("dbo.ProzBevPassiv", "VerfahrensId");
            CreateIndex("dbo.ProzBevBeigeladen", "VerfahrensId");
            CreateIndex("dbo.ProzBevAktiv", "VerfahrensId");
            CreateIndex("dbo.ParteienZeugen", "VerfahrensId");
            CreateIndex("dbo.ParteienSV", "VerfahrensId");
            CreateIndex("dbo.ParteienPassiv", "VerfahrensId");
            CreateIndex("dbo.ParteienBeigeladen", "VerfahrensId");
            CreateIndex("dbo.ParteienAktiv", "VerfahrensId");
            CreateIndex("dbo.Besetzung", "VerfahrensId");
            AddForeignKey("dbo.ProzBevPassiv", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ProzBevBeigeladen", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ProzBevAktiv", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ParteienZeugen", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ParteienSV", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ParteienPassiv", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ParteienBeigeladen", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.ParteienAktiv", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
            AddForeignKey("dbo.Besetzung", "VerfahrensId", "dbo.Verfahren", "VerfahrensId", cascadeDelete: true);
        }
    }
}
