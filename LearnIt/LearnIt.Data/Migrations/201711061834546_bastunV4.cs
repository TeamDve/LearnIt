namespace LearnIt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bastunV4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ImageBase", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "ImageBase");
        }
    }
}
