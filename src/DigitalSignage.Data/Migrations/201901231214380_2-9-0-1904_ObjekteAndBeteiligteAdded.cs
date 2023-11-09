// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
namespace DigitalSignage.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2901904_ObjekteAndBeteiligteAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Objekte",
                c => new
                    {
                        ObjektId = c.Int(nullable: false, identity: true),
                        VerfahrensId = c.Long(nullable: false),
                        Objektart = c.String(nullable: false, maxLength: 255),
                        Gemarkung = c.String(maxLength: 255),
                        Flur = c.String(maxLength: 255),
                        Wirtschaftsart = c.String(maxLength: 255),
                        Anschrift = c.String(maxLength: 255),
                        Groesse = c.String(maxLength: 50),
                        Objekt = c.String(maxLength: 255),
                        Blatt = c.String(maxLength: 50),
                        Grundbuchamt = c.String(maxLength: 255),
                        Zusatz = c.String(maxLength: 255),
                        Eigentumsart = c.String(maxLength: 255),
                        Nutzungsrecht = c.String(maxLength: 255),
                        Kurzname = c.String(maxLength: 255),
                        Verkehrswert = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Grundbuchbezirk = c.String(maxLength: 255),
                        Eigentumsanteil = c.String(maxLength: 50),
                        Schiffsregisterart = c.String(maxLength: 50),
                        Schiffsname = c.String(maxLength: 255),
                        IMONR = c.String(maxLength: 255),
                        RegisterGericht = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ObjektId)
                .ForeignKey("dbo.Verfahren", t => t.VerfahrensId, cascadeDelete: true)
                .Index(t => t.VerfahrensId);
            
            CreateTable(
                "dbo.ParteienBeteiligt",
                c => new
                    {
                        ParteiId = c.Int(nullable: false, identity: true),
                        VerfahrensId = c.Long(nullable: false),
                        Partei = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ParteiId)
                .ForeignKey("dbo.Verfahren", t => t.VerfahrensId, cascadeDelete: true)
                .Index(t => t.VerfahrensId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParteienBeteiligt", "VerfahrensId", "dbo.Verfahren");
            DropForeignKey("dbo.Objekte", "VerfahrensId", "dbo.Verfahren");
            DropIndex("dbo.ParteienBeteiligt", new[] { "VerfahrensId" });
            DropIndex("dbo.Objekte", new[] { "VerfahrensId" });
            DropTable("dbo.ParteienBeteiligt");
            DropTable("dbo.Objekte");
        }
    }
}
