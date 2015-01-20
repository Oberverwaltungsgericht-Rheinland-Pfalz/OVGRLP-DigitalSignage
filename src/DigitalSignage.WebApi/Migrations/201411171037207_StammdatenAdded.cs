namespace DigitalSignage.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StammdatenAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stammdaten",
                c => new
                    {
                        StammdatenId = c.Int(nullable: false, identity: true),
                        Gerichtsname = c.String(nullable: false, maxLength: 255),
                        Datum = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.StammdatenId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stammdaten");
        }
    }
}
