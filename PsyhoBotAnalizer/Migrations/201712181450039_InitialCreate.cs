namespace PsyhoBotAnalizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        QuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dialogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        PatientId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Diagnosis = c.String(),
                        Sex = c.String(),
                        DiseasePeriod = c.String(),
                        NormalCondition = c.String(),
                        Characteristic = c.String(),
                        DoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Sex = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Category = c.String(),
                        Charasteristic = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.DialogQuestions",
                c => new
                    {
                        Dialog_Id = c.Int(nullable: false),
                        Question_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Dialog_Id, t.Question_Id })
                .ForeignKey("dbo.Dialogs", t => t.Dialog_Id, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Question_Id, cascadeDelete: true)
                .Index(t => t.Dialog_Id)
                .Index(t => t.Question_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionsResults", "DialogId", "dbo.Dialogs");
            DropForeignKey("dbo.DialogQuestions", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.DialogQuestions", "Dialog_Id", "dbo.Dialogs");
            DropForeignKey("dbo.Patients", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Dialogs", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.DialogQuestions", new[] { "Question_Id" });
            DropIndex("dbo.DialogQuestions", new[] { "Dialog_Id" });
            DropIndex("dbo.QuestionsResults", new[] { "DialogId" });
            DropIndex("dbo.Patients", new[] { "DoctorId" });
            DropIndex("dbo.Dialogs", new[] { "PatientId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.DialogQuestions");
            DropTable("dbo.QuestionsResults");
            DropTable("dbo.Doctors");
            DropTable("dbo.Patients");
            DropTable("dbo.Dialogs");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
