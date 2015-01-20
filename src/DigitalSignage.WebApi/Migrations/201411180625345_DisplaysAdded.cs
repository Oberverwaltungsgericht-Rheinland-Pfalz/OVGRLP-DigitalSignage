namespace DigitalSignage.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisplaysAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Displays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Room = c.String(),
                        Department = c.String(),
                        Group = c.String(),
                        ControlUrl = c.String(),
                        NetAddress = c.String(),
                        MacAddress = c.String(),
                        UdpPort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Displays");
        }
    }
}
