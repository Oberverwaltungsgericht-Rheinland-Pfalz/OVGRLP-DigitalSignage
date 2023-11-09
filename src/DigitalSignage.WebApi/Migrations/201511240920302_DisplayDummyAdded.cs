// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
namespace DigitalSignage.WebApi.Migrations
{
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