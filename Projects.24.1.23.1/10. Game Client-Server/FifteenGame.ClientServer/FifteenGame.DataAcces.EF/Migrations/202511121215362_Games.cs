namespace FifteenGame.DataAcces.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Games : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.GameCells",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        Row = c.Int(nullable: false),
                        Column = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "public.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MoveCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.GameCells", "GameId", "public.Games");
            DropForeignKey("public.Games", "UserId", "public.Users");
            DropIndex("public.Games", new[] { "UserId" });
            DropIndex("public.GameCells", new[] { "GameId" });
            DropTable("public.Games");
            DropTable("public.GameCells");
        }
    }
}
