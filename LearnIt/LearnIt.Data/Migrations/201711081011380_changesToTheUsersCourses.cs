namespace LearnIt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesToTheUsersCourses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserCourses", "AssignmentDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserCourses", "CompletionDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserCourses", "CompletionDate");
            DropColumn("dbo.UserCourses", "AssignmentDate");
        }
    }
}
