namespace LearnIt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bastunV7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "RoleId", "dbo.Roles");
            DropIndex("dbo.AspNetUsers", new[] { "RoleId" });
            AlterColumn("dbo.AspNetUsers", "RoleId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "RoleId");
            AddForeignKey("dbo.AspNetUsers", "RoleId", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "RoleId", "dbo.Roles");
            DropIndex("dbo.AspNetUsers", new[] { "RoleId" });
            AlterColumn("dbo.AspNetUsers", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "RoleId");
            AddForeignKey("dbo.AspNetUsers", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}
