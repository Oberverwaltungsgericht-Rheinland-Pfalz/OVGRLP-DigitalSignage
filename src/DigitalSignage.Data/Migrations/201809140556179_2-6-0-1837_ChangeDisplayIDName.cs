namespace DigitalSignage.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2601837_ChangeDisplayIDName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NoteAssignments", "Display_Id", "dbo.Displays");
            DropIndex("dbo.NoteAssignments", new[] { "Display_Id" });
            RenameColumn(table: "dbo.NoteAssignments", name: "Display_Id", newName: "DisplayId");
            AlterColumn("dbo.NoteAssignments", "DisplayId", c => c.Int(nullable: false));
            CreateIndex("dbo.NoteAssignments", "DisplayId");
            AddForeignKey("dbo.NoteAssignments", "DisplayId", "dbo.Displays", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NoteAssignments", "DisplayId", "dbo.Displays");
            DropIndex("dbo.NoteAssignments", new[] { "DisplayId" });
            AlterColumn("dbo.NoteAssignments", "DisplayId", c => c.Int());
            RenameColumn(table: "dbo.NoteAssignments", name: "DisplayId", newName: "Display_Id");
            CreateIndex("dbo.NoteAssignments", "Display_Id");
            AddForeignKey("dbo.NoteAssignments", "Display_Id", "dbo.Displays", "Id");
        }
    }
}
