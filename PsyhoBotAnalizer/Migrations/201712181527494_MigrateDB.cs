namespace PsyhoBotAnalizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionsResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Answer = c.String(),
                        DialogId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dialogs", t => t.DialogId)
                .Index(t => t.DialogId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionsResults", "DialogId", "dbo.Dialogs");
            DropIndex("dbo.QuestionsResults", new[] { "DialogId" });
            DropTable("dbo.QuestionsResults");
        }
    }
}
