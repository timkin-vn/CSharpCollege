namespace FifteenGame.DataAcces.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Match3Game : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Games", "MatchesCount", c => c.Int(nullable: false));
            AddColumn("public.Games", "IsFinished", c => c.Boolean(nullable: false));
            DropColumn("public.Games", "MoveCount");
        }
        
        public override void Down()
        {
            AddColumn("public.Games", "MoveCount", c => c.Int(nullable: false));
            DropColumn("public.Games", "IsFinished");
            DropColumn("public.Games", "MatchesCount");
        }
    }
}
