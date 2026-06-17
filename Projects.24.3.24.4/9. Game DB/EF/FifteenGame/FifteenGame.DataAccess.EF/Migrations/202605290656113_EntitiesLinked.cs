namespace FifteenGame.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesLinked : DbMigration
    {
        public override void Up()
        {
            CreateIndex("public.Cells", "GameId");
            CreateIndex("public.Games", "UserId");
            AddForeignKey("public.Cells", "GameId", "public.Games", "Id", cascadeDelete: true);
            AddForeignKey("public.Games", "UserId", "public.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("public.Games", "UserId", "public.Users");
            DropForeignKey("public.Cells", "GameId", "public.Games");
            DropIndex("public.Games", new[] { "UserId" });
            DropIndex("public.Cells", new[] { "GameId" });
        }
    }
}
