namespace PacmanGame.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddCreationDate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameStateEntity", "GameUserId", "dbo.GameUserEntity");
            DropPrimaryKey("dbo.GameStateEntity");
            DropPrimaryKey("dbo.GameUserEntity");

            AddColumn("dbo.GameStateEntity", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.GameStateEntity", "Lives", c => c.Int(nullable: false));
            AddColumn("dbo.GameStateEntity", "PlayerX", c => c.Int(nullable: false));
            AddColumn("dbo.GameStateEntity", "PlayerY", c => c.Int(nullable: false));
            AddColumn("dbo.GameStateEntity", "BoardState", c => c.String(maxLength: 4000));
            AddColumn("dbo.GameStateEntity", "GhostsPositions", c => c.String(maxLength: 200));

            // ИСПРАВЛЕНИЕ: Добавляем defaultValueSql для заполнения существующих строк текущим временем (NOW() для PostgreSQL)
            AddColumn("dbo.GameStateEntity", "CreatedAt", c => c.DateTime(nullable: false, defaultValueSql: "NOW()"));

            AddColumn("dbo.GameUserEntity", "Id", c => c.Int(nullable: false, identity: true));

            // ИСПРАВЛЕНИЕ: Добавляем defaultValueSql для заполнения существующих строк текущим временем (NOW() для PostgreSQL)
            AddColumn("dbo.GameUserEntity", "CreatedAt", c => c.DateTime(nullable: false, defaultValueSql: "NOW()"));

            AlterColumn("dbo.GameUserEntity", "Password", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.GameStateEntity", "Id");
            AddPrimaryKey("dbo.GameUserEntity", "Id");
            AddForeignKey("dbo.GameStateEntity", "GameUserId", "dbo.GameUserEntity", "Id", cascadeDelete: true);

            DropColumn("dbo.GameStateEntity", "GameStateId");
            DropColumn("dbo.GameStateEntity", "StateData");
            DropColumn("dbo.GameStateEntity", "SaveDate");
            DropColumn("dbo.GameUserEntity", "GameUserId");
        }

        public override void Down()
        {
            AddColumn("dbo.GameUserEntity", "GameUserId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.GameStateEntity", "SaveDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.GameStateEntity", "StateData", c => c.String());
            AddColumn("dbo.GameStateEntity", "GameStateId", c => c.Int(nullable: false, identity: true));

            DropForeignKey("dbo.GameStateEntity", "GameUserId", "dbo.GameUserEntity");
            DropPrimaryKey("dbo.GameUserEntity");
            DropPrimaryKey("dbo.GameStateEntity");

            AlterColumn("dbo.GameUserEntity", "Password", c => c.String());

            // При откате миграции нам не нужно defaultValueSql, так как мы удаляем столбец
            DropColumn("dbo.GameUserEntity", "CreatedAt");

            DropColumn("dbo.GameUserEntity", "Id");

            // При откате миграции нам не нужно defaultValueSql, так как мы удаляем столбец
            DropColumn("dbo.GameStateEntity", "CreatedAt");

            DropColumn("dbo.GameStateEntity", "GhostsPositions");
            DropColumn("dbo.GameStateEntity", "BoardState");
            DropColumn("dbo.GameStateEntity", "PlayerY");
            DropColumn("dbo.GameStateEntity", "PlayerX");
            DropColumn("dbo.GameStateEntity", "Lives");
            DropColumn("dbo.GameStateEntity", "Id");

            AddPrimaryKey("dbo.GameUserEntity", "GameUserId");
            AddPrimaryKey("dbo.GameStateEntity", "GameStateId");
            AddForeignKey("dbo.GameStateEntity", "GameUserId", "dbo.GameUserEntity", "GameUserId", cascadeDelete: true);
        }
    }
}