namespace DigitalSignage.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisplayChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Displays", "WolIpAddress", c => c.String());
            AddColumn("dbo.Displays", "WolMacAddress", c => c.String());
            AddColumn("dbo.Displays", "WolUdpPort", c => c.Int(nullable: false));
            AddColumn("dbo.Displays", "Description", c => c.String());
            DropColumn("dbo.Displays", "MacAddress");
            DropColumn("dbo.Displays", "UdpPort");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Displays", "UdpPort", c => c.Int(nullable: false));
            AddColumn("dbo.Displays", "MacAddress", c => c.String());
            DropColumn("dbo.Displays", "Description");
            DropColumn("dbo.Displays", "WolUdpPort");
            DropColumn("dbo.Displays", "WolMacAddress");
            DropColumn("dbo.Displays", "WolIpAddress");
        }
    }
}
