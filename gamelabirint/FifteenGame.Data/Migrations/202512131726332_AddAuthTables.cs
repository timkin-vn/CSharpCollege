namespace FifteenGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.GameStates", "BoardData", c => c.String());
            AddColumn("public.GameStates", "Score", c => c.Int(nullable: false));
            AddColumn("public.Users", "Username", c => c.String());
            DropColumn("public.GameStates", "MazeJson");
            DropColumn("public.GameStates", "PlayerX");
            DropColumn("public.GameStates", "PlayerY");
            DropColumn("public.GameStates", "MovesCount");
            DropColumn("public.Users", "Login");
        }
        
        public override void Down()
        {
            AddColumn("public.Users", "Login", c => c.String());
            AddColumn("public.GameStates", "MovesCount", c => c.Int(nullable: false));
            AddColumn("public.GameStates", "PlayerY", c => c.Int(nullable: false));
            AddColumn("public.GameStates", "PlayerX", c => c.Int(nullable: false));
            AddColumn("public.GameStates", "MazeJson", c => c.String());
            DropColumn("public.Users", "Username");
            DropColumn("public.GameStates", "Score");
            DropColumn("public.GameStates", "BoardData");
        }
    }
}
