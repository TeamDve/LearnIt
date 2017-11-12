namespace LearnIt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCoursesChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserCourses", "IsMandatory", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UserCourses", "CompletionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserCourses", "CompletionDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserCourses", "IsMandatory");
        }
    }
}
