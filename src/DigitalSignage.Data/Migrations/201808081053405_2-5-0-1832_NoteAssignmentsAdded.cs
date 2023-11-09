// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
namespace DigitalSignage.Data.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class _2501832_NoteAssignmentsAdded : DbMigration
  {
    public override void Up()
    {
      DropForeignKey("dbo.NoteDisplays", "Note_Id", "dbo.Notes");
      DropForeignKey("dbo.NoteDisplays", "Display_Id", "dbo.Displays");
      DropIndex("dbo.NoteDisplays", new[] { "Note_Id" });
      DropIndex("dbo.NoteDisplays", new[] { "Display_Id" });
      CreateTable(
          "dbo.NoteAssignments",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            NoteId = c.Int(nullable: false),
            Start = c.DateTime(),
            End = c.DateTime(),
            Comment = c.String(),
            Display_Id = c.Int(),
          })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Notes", t => t.NoteId, cascadeDelete: true)
          .ForeignKey("dbo.Displays", t => t.Display_Id)
          .Index(t => t.NoteId)
          .Index(t => t.Display_Id);

      DropColumn("dbo.Notes", "Start");
      DropColumn("dbo.Notes", "End");
      DropTable("dbo.NoteDisplays");
    }

    public override void Down()
    {
      CreateTable(
          "dbo.NoteDisplays",
          c => new
          {
            Note_Id = c.Int(nullable: false),
            Display_Id = c.Int(nullable: false),
          })
          .PrimaryKey(t => new { t.Note_Id, t.Display_Id });

      AddColumn("dbo.Notes", "End", c => c.DateTime());
      AddColumn("dbo.Notes", "Start", c => c.DateTime());
      DropForeignKey("dbo.NoteAssignments", "Display_Id", "dbo.Displays");
      DropForeignKey("dbo.NoteAssignments", "NoteId", "dbo.Notes");
      DropIndex("dbo.NoteAssignments", new[] { "Display_Id" });
      DropIndex("dbo.NoteAssignments", new[] { "NoteId" });
      DropTable("dbo.NoteAssignments");
      CreateIndex("dbo.NoteDisplays", "Display_Id");
      CreateIndex("dbo.NoteDisplays", "Note_Id");
      AddForeignKey("dbo.NoteDisplays", "Display_Id", "dbo.Displays", "Id", cascadeDelete: true);
      AddForeignKey("dbo.NoteDisplays", "Note_Id", "dbo.Notes", "Id", cascadeDelete: true);
    }
  }
}
