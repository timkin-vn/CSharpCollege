namespace FifteenGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.GameStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MazeJson = c.String(),
                        PlayerX = c.Int(nullable: false),
                        PlayerY = c.Int(nullable: false),
                        MovesCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "public.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.GameStates", "UserId", "public.Users");
            DropIndex("public.GameStates", new[] { "UserId" });
            DropTable("public.Users");
            DropTable("public.GameStates");
        }
    }
}
