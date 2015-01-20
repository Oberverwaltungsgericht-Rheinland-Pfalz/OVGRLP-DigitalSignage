namespace DigitalSignage.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisplayStylesAndTemplateAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Displays", "Template", c => c.String());
            AddColumn("dbo.Displays", "Styles", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Displays", "Styles");
            DropColumn("dbo.Displays", "Template");
        }
    }
}
