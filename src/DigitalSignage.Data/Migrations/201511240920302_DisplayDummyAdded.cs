namespace DigitalSignage.Data.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class DisplayDummyAdded : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.Displays", "Dummy", c => c.Boolean(nullable: false));
    }

    public override void Down()
    {
      DropColumn("dbo.Displays", "Dummy");
    }
  }
}
