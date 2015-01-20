namespace DigitalSignage.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisplaySettingsTitleAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Displays", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Displays", "Title");
        }
    }
}
