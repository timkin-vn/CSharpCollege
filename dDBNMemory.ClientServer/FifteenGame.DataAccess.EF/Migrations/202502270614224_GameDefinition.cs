namespace FifteenGame.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameDefinition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Cells",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Row = c.Int(nullable: false),
                        Column = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "public.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MoveCount = c.Int(nullable: false),
                        GameStart = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.Games", "User_Id", "public.Users");
            DropForeignKey("public.Cells", "Game_Id", "public.Games");
            DropIndex("public.Games", new[] { "User_Id" });
            DropIndex("public.Cells", new[] { "Game_Id" });
            DropTable("public.Games");
            DropTable("public.Cells");
        }
    }
}
