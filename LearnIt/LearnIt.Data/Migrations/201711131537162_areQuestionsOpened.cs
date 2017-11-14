namespace LearnIt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class areQuestionsOpened : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserCourses", "areQuestionsOpened", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserCourses", "areQuestionsOpened");
        }
    }
}
