namespace DigitalSignage.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Content = c.String(),
                        Start = c.DateTime(),
                        End = c.DateTime(),
                        Forced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NoteDisplays",
                c => new
                    {
                        Note_Id = c.Int(nullable: false),
                        Display_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Note_Id, t.Display_Id })
                .ForeignKey("dbo.Notes", t => t.Note_Id, cascadeDelete: true)
                .ForeignKey("dbo.Displays", t => t.Display_Id, cascadeDelete: true)
                .Index(t => t.Note_Id)
                .Index(t => t.Display_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NoteDisplays", "Display_Id", "dbo.Displays");
            DropForeignKey("dbo.NoteDisplays", "Note_Id", "dbo.Notes");
            DropIndex("dbo.NoteDisplays", new[] { "Display_Id" });
            DropIndex("dbo.NoteDisplays", new[] { "Note_Id" });
            DropTable("dbo.NoteDisplays");
            DropTable("dbo.Notes");
        }
    }
}
