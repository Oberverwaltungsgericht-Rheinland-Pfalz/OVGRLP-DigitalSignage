namespace DigitalSignage.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VerfahrenBesetzungAdded : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Besetzung", "VerfahrensId", "dbo.Verfahren");
            DropIndex("dbo.Besetzung", new[] { "VerfahrensId" });
            DropTable("dbo.Besetzung");
        }
    }
}
