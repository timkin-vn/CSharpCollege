namespace FifteenGame.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameMinorFixes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("public.Cells", "Game_Id", "public.Games");
            DropForeignKey("public.Games", "User_Id", "public.Users");
            DropIndex("public.Cells", new[] { "Game_Id" });
            DropIndex("public.Games", new[] { "User_Id" });
            RenameColumn(table: "public.Cells", name: "Game_Id", newName: "GameId");
            RenameColumn(table: "public.Games", name: "User_Id", newName: "UserId");
            AlterColumn("public.Cells", "GameId", c => c.Int(nullable: false));
            AlterColumn("public.Games", "UserId", c => c.Int(nullable: false));
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
            AlterColumn("public.Games", "UserId", c => c.Int());
            AlterColumn("public.Cells", "GameId", c => c.Int());
            RenameColumn(table: "public.Games", name: "UserId", newName: "User_Id");
            RenameColumn(table: "public.Cells", name: "GameId", newName: "Game_Id");
            CreateIndex("public.Games", "User_Id");
            CreateIndex("public.Cells", "Game_Id");
            AddForeignKey("public.Games", "User_Id", "public.Users", "Id");
            AddForeignKey("public.Cells", "Game_Id", "public.Games", "Id");
        }
    }
}
