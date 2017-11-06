namespace LearnIt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bastunV2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Departments_Id", "dbo.Departments");
            DropIndex("dbo.AspNetUsers", new[] { "Departments_Id" });
            DropColumn("dbo.AspNetUsers", "DepartmentId");
            RenameColumn(table: "dbo.AspNetUsers", name: "Departments_Id", newName: "DepartmentId");
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Required = c.Boolean(nullable: false),
                        Description = c.String(nullable: false, maxLength: 150),
                        ScoreToPass = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qstn = c.String(nullable: false, maxLength: 150),
                        Answers = c.String(nullable: false, maxLength: 150),
                        RightAnswer = c.String(nullable: false, maxLength: 150),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            AlterColumn("dbo.AspNetUsers", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "DepartmentId");
            AddForeignKey("dbo.AspNetUsers", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Questions", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Images", "CourseId", "dbo.Courses");
            DropIndex("dbo.AspNetUsers", new[] { "DepartmentId" });
            DropIndex("dbo.Questions", new[] { "CourseId" });
            DropIndex("dbo.Images", new[] { "CourseId" });
            AlterColumn("dbo.AspNetUsers", "DepartmentId", c => c.Int());
            DropTable("dbo.Questions");
            DropTable("dbo.Images");
            DropTable("dbo.Courses");
            RenameColumn(table: "dbo.AspNetUsers", name: "DepartmentId", newName: "Departments_Id");
            AddColumn("dbo.AspNetUsers", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Departments_Id");
            AddForeignKey("dbo.AspNetUsers", "Departments_Id", "dbo.Departments", "Id");
        }
    }
}
