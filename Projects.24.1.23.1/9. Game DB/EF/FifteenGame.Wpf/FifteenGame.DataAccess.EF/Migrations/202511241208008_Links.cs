namespace FifteenGame.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Links : DbMigration
    {
        public override void Up()
        {
            AlterColumn("public.Users", "Name", c => c.String(nullable: false));
            CreateIndex("public.GameCells", "GameId");
            CreateIndex("public.Games", "UserId");
            AddForeignKey("public.GameCells", "GameId", "public.Games", "Id", cascadeDelete: true);
            AddForeignKey("public.Games", "UserId", "public.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("public.Games", "UserId", "public.Users");
            DropForeignKey("public.GameCells", "GameId", "public.Games");
            DropIndex("public.Games", new[] { "UserId" });
            DropIndex("public.GameCells", new[] { "GameId" });
            AlterColumn("public.Users", "Name", c => c.String());
        }
    }
}
