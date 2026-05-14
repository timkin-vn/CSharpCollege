namespace Minesweeper.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.game_states",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        game_data = c.String(nullable: false),
                        is_game_over = c.Boolean(nullable: false),
                        is_game_won = c.Boolean(nullable: false),
                        play_time = c.Time(nullable: false, precision: 6),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        field_size = c.Int(nullable: false),
                        mine_count = c.Int(nullable: false),
                        flags_placed = c.Int(nullable: false),
                        cells_revealed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("public.users", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id);
            
            CreateTable(
                "public.users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false, maxLength: 100),
                        created_at = c.DateTime(nullable: false),
                        total_games_played = c.Int(nullable: false),
                        games_won = c.Int(nullable: false),
                        last_played_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.game_states", "user_id", "public.users");
            DropIndex("public.game_states", new[] { "user_id" });
            DropTable("public.users");
            DropTable("public.game_states");
        }
    }
}
