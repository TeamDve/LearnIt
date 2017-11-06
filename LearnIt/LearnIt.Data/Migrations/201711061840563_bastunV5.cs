namespace LearnIt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bastunV5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ImageBinary", c => c.Binary());
            DropColumn("dbo.Images", "ImageBase");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ImageBase", c => c.String(unicode: false));
            DropColumn("dbo.Images", "ImageBinary");
        }
    }
}
