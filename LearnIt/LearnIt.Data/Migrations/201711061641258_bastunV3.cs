namespace LearnIt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bastunV3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        DueDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.UserCourses", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserCourses", new[] { "UserId" });
            DropIndex("dbo.UserCourses", new[] { "CourseId" });
            DropTable("dbo.UserCourses");
        }
    }
}
