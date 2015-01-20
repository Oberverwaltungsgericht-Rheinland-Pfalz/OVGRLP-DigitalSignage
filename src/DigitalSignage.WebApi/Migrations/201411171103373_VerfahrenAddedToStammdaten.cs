namespace DigitalSignage.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VerfahrenAddedToStammdaten : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Verfahren", "StammdatenId");
            AddForeignKey("dbo.Verfahren", "StammdatenId", "dbo.Stammdaten", "StammdatenId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Verfahren", "StammdatenId", "dbo.Stammdaten");
            DropIndex("dbo.Verfahren", new[] { "StammdatenId" });
        }
    }
}
