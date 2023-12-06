namespace QMSL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        Text = c.String(),
                        type = c.Int(nullable: false),
                        CommentedAt = c.DateTime(nullable: false),
                        EditablePollId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EditablePolls", t => t.EditablePollId, cascadeDelete: true)
                .Index(t => t.EditablePollId);
            
            CreateTable(
                "dbo.EditablePolls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Fathername = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Age = c.Int(nullable: false),
                        Sex = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Fathername = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Age = c.Int(nullable: false),
                        Sex = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GeneralPolls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DoctorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.GeneralQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GeneralPollId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GeneralPolls", t => t.GeneralPollId, cascadeDelete: true)
                .Index(t => t.GeneralPollId);
            
            CreateTable(
                "dbo.EditableQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ChoosenAnswer = c.Int(),
                        EditablePollId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EditablePolls", t => t.EditablePollId, cascadeDelete: true)
                .Index(t => t.EditablePollId);
            
            CreateTable(
                "dbo.DoctorPatients",
                c => new
                    {
                        Doctor_Id = c.Int(nullable: false),
                        Patient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Doctor_Id, t.Patient_Id })
                .ForeignKey("dbo.Doctors", t => t.Doctor_Id, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.Patient_Id, cascadeDelete: true)
                .Index(t => t.Doctor_Id)
                .Index(t => t.Patient_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EditableQuestions", "EditablePollId", "dbo.EditablePolls");
            DropForeignKey("dbo.EditablePolls", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.GeneralQuestions", "GeneralPollId", "dbo.GeneralPolls");
            DropForeignKey("dbo.GeneralPolls", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.DoctorPatients", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.DoctorPatients", "Doctor_Id", "dbo.Doctors");
            DropForeignKey("dbo.Comments", "EditablePollId", "dbo.EditablePolls");
            DropIndex("dbo.DoctorPatients", new[] { "Patient_Id" });
            DropIndex("dbo.DoctorPatients", new[] { "Doctor_Id" });
            DropIndex("dbo.EditableQuestions", new[] { "EditablePollId" });
            DropIndex("dbo.GeneralQuestions", new[] { "GeneralPollId" });
            DropIndex("dbo.GeneralPolls", new[] { "DoctorId" });
            DropIndex("dbo.EditablePolls", new[] { "PatientId" });
            DropIndex("dbo.Comments", new[] { "EditablePollId" });
            DropTable("dbo.DoctorPatients");
            DropTable("dbo.EditableQuestions");
            DropTable("dbo.GeneralQuestions");
            DropTable("dbo.GeneralPolls");
            DropTable("dbo.Doctors");
            DropTable("dbo.Patients");
            DropTable("dbo.EditablePolls");
            DropTable("dbo.Comments");
        }
    }
}
