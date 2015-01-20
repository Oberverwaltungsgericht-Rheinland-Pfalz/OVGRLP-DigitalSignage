namespace DigitalSignage.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicsAdded : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Basics");
        }
    }
}
