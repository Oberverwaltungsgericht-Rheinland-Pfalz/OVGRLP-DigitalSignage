namespace DigitalSignage.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisplaySettingsChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Displays", "Filter", c => c.String());
            DropColumn("dbo.Displays", "Room");
            DropColumn("dbo.Displays", "Department");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Displays", "Department", c => c.String());
            AddColumn("dbo.Displays", "Room", c => c.String());
            DropColumn("dbo.Displays", "Filter");
        }
    }
}
