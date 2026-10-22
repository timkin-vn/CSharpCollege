namespace FifteenGame.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckersTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.CheckersGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        CurrentPlayer = c.Int(nullable: false),
                        IsFinished = c.Boolean(nullable: false),
                        Winner = c.Int(),
                        GameStateJson = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "public.CheckersMoves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        MoveNumber = c.Int(nullable: false),
                        FromRow = c.Int(nullable: false),
                        FromCol = c.Int(nullable: false),
                        ToRow = c.Int(nullable: false),
                        ToCol = c.Int(nullable: false),
                        IsCapture = c.Boolean(nullable: false),
                        CapturedRow = c.Int(),
                        CapturedCol = c.Int(),
                        PromotedToKing = c.Boolean(nullable: false),
                        MoveTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.CheckersGames", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.CheckersGames", "UserId", "public.Users");
            DropForeignKey("public.CheckersMoves", "GameId", "public.CheckersGames");
            DropIndex("public.CheckersMoves", new[] { "GameId" });
            DropIndex("public.CheckersGames", new[] { "UserId" });
            DropTable("public.CheckersMoves");
            DropTable("public.CheckersGames");
        }
    }
}
