namespace DigitalSignage.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2701839_PermissionsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ressource = c.String(nullable: false),
                        Member = c.String(nullable: false),
                        GET = c.Boolean(nullable: false),
                        PUT = c.Boolean(nullable: false),
                        POST = c.Boolean(nullable: false),
                        DELETE = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Permissions");
        }
    }
}
