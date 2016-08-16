namespace DigitalSignage.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SitzungssaalNrNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Verfahren", "SitzungssaalNr", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Verfahren", "SitzungssaalNr", c => c.Int(nullable: false));
        }
    }
}
