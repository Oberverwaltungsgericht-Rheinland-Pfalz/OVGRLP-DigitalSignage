namespace DigitalSignage.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class SitzungssaalNrAdded : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.Verfahren", "SitzungssaalNr", c => c.Int());
    }

    public override void Down()
    {
      DropColumn("dbo.Verfahren", "SitzungssaalNr");
    }
  }
}