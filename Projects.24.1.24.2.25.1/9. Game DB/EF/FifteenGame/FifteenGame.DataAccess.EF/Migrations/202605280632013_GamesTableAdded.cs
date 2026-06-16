namespace FifteenGame.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GamesTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Cells",
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
            DropForeignKey("public.Games", "UserId", "public.Users");
            DropForeignKey("public.Cells", "GameId", "public.Games");
            DropIndex("public.Games", new[] { "UserId" });
            DropIndex("public.Cells", new[] { "GameId" });
            DropTable("public.Games");
            DropTable("public.Cells");
        }
    }
}
