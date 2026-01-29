using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.DataAccess.EF.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.users",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    name = c.String(nullable: false, maxLength: 255),
                    created_at = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.id)
                .Index(t => t.name, unique: true);

            CreateTable(
                "public.maps",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    name = c.String(nullable: false, maxLength: 255),
                    row_count = c.Int(nullable: false),
                    col_count = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.id)
                .Index(t => t.name, unique: true);

            CreateTable(
                "public.map_cells",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    map_id = c.Int(nullable: false),
                    row = c.Int(nullable: false),
                    col = c.Int(nullable: false),
                    cell_type = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("public.maps", t => t.map_id, cascadeDelete: true)
                .Index(t => new { t.map_id, t.row, t.col }, unique: true);

            CreateTable(
                "public.games",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    user_id = c.Int(nullable: false),
                    map_id = c.Int(nullable: false),
                    status = c.Int(nullable: false),
                    score = c.Int(nullable: false),
                    lives = c.Int(nullable: false),
                    created_at = c.DateTime(nullable: false),
                    updated_at = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("public.users", t => t.user_id, cascadeDelete: true)
                .ForeignKey("public.maps", t => t.map_id, cascadeDelete: true)
                .Index(t => t.user_id);

            CreateTable(
                "public.game_actors",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    game_id = c.Int(nullable: false),
                    actor_type = c.Int(nullable: false),
                    row = c.Int(nullable: false),
                    col = c.Int(nullable: false),
                    direction = c.Int(nullable: false),
                    frightened_ticks_left = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("public.games", t => t.game_id, cascadeDelete: true)
                .Index(t => new { t.game_id, t.actor_type }, unique: true)
                .Index(t => t.game_id);

            CreateTable(
                "public.game_collectible_state",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    game_id = c.Int(nullable: false),
                    row = c.Int(nullable: false),
                    col = c.Int(nullable: false),
                    collectible_type = c.Int(nullable: false),
                    is_eaten = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("public.games", t => t.game_id, cascadeDelete: true)
                .Index(t => new { t.game_id, t.row, t.col }, unique: true)
                .Index(t => t.game_id);
        }

        public override void Down()
        {
            DropForeignKey("public.game_collectible_state", "game_id", "public.games");
            DropForeignKey("public.game_actors", "game_id", "public.games");
            DropForeignKey("public.games", "map_id", "public.maps");
            DropForeignKey("public.games", "user_id", "public.users");
            DropForeignKey("public.map_cells", "map_id", "public.maps");
            DropIndex("public.game_collectible_state", new[] { "game_id", "row", "col" });
            DropIndex("public.game_collectible_state", new[] { "game_id" });
            DropIndex("public.game_actors", new[] { "game_id", "actor_type" });
            DropIndex("public.game_actors", new[] { "game_id" });
            DropIndex("public.games", new[] { "user_id" });
            DropIndex("public.map_cells", new[] { "map_id", "row", "col" });
            DropIndex("public.maps", new[] { "name" });
            DropIndex("public.users", new[] { "name" });
            DropTable("public.game_collectible_state");
            DropTable("public.game_actors");
            DropTable("public.games");
            DropTable("public.map_cells");
            DropTable("public.maps");
            DropTable("public.users");
        }
    }
}