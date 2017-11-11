namespace LearnIt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseNamesCHanges : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Courses", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Courses", new[] { "Name" });
        }
    }
}
