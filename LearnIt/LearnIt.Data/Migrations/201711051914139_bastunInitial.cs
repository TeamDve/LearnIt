namespace LearnIt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bastunInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "RoleId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "DepartmentId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Departments_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "RoleId");
            CreateIndex("dbo.AspNetUsers", "Departments_Id");
            AddForeignKey("dbo.AspNetUsers", "Departments_Id", "dbo.Departments", "Id");
            AddForeignKey("dbo.AspNetUsers", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.AspNetUsers", "Departments_Id", "dbo.Departments");
            DropIndex("dbo.AspNetUsers", new[] { "Departments_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "RoleId" });
            DropColumn("dbo.AspNetUsers", "Departments_Id");
            DropColumn("dbo.AspNetUsers", "DepartmentId");
            DropColumn("dbo.AspNetUsers", "RoleId");
            DropTable("dbo.Roles");
            DropTable("dbo.Departments");
        }
    }
}
