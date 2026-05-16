namespace PacmanGame.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalModelSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameStateEntity",
                c => new
                    {
                        GameStateId = c.Int(nullable: false, identity: true),
                        GameUserId = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        IsGameOver = c.Boolean(nullable: false),
                        Score = c.Int(nullable: false),
                        StateData = c.String(),
                        SaveDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GameStateId)
                .ForeignKey("dbo.GameUserEntity", t => t.GameUserId, cascadeDelete: true)
                .Index(t => t.GameUserId);
            
            CreateTable(
                "dbo.GameUserEntity",
                c => new
                    {
                        GameUserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 100),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.GameUserId)
                .Index(t => t.Username, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameStateEntity", "GameUserId", "dbo.GameUserEntity");
            DropIndex("dbo.GameUserEntity", new[] { "Username" });
            DropIndex("dbo.GameStateEntity", new[] { "GameUserId" });
            DropTable("dbo.GameUserEntity");
            DropTable("dbo.GameStateEntity");
        }
    }
}
